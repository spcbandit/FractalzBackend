using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Fractalz.Application.Handlers.User;

public class DigSignGetHandler:IRequestHandler<DigSignGetRequest, DigSignGetResponse>
{
    public string hashedPassword { get; set; }
    private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
    public DigSignGetHandler(IRepository<Domains.Entities.Profile.User> repositoryUser, IConfiguration configuration)
    {
        _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
    }
    public async Task<DigSignGetResponse> Handle(DigSignGetRequest request, CancellationToken cancellationToken)
    {

        if (_repositoryUser.Get(x => x.Login == request.Login || request.Login == x.Email).FirstOrDefault() != null)
        {
            HashMethod(request);
            var user = _repositoryUser.GetWithInclude(x=> x.Login == request.Login && x.Password == hashedPassword).FirstOrDefault();

            if (user == null)
            { return new DigSignGetResponse() { Message = MessageResource.User_passwordWron, Success = false}; }
            if (user.IsEmailConfirmed == 0)
            { return new DigSignGetResponse() { Message = MessageResource.User_codeValidWron, Success = false}; }
            else
            {
                return FileCreate(request, user);
            }
        }
        else
        { return new DigSignGetResponse() { Message = MessageResource.User_loginWron, Success = false }; }
        
    }

    private DigSignGetResponse FileCreate(DigSignGetRequest request, object user)
    {
        string UserSignsRepos = Environment.CurrentDirectory + $"/Digital sign users repository";

        if (!Directory.Exists(UserSignsRepos))
        {
            Directory.CreateDirectory(UserSignsRepos);
        }
        
        string infToSend;
        infToSend = JsonConvert.SerializeObject(user);

        using (FileStream fstream = new FileStream(Environment.CurrentDirectory + $"/Digital sign users repository/{request.OrganizationName}", FileMode.Create))
        {
            
            byte[] buffer = Encoding.Default.GetBytes(infToSend);
            fstream.WriteAsync(buffer, 0, buffer.Length);
        }
        byte[] fileBytes = System.IO.File.ReadAllBytes(Environment.CurrentDirectory + $"/Digital sign users repository/{request.OrganizationName}");
            
        var memoryStream = new MemoryStream(fileBytes);

        if (memoryStream.Length != 0)
        {
            return new DigSignGetResponse() 
            {
                Success = true, 
                FileStreamResult = new FileStreamResult(memoryStream, "application/octet-stream"){FileDownloadName = request.OrganizationName}
            };
            
        }
        else
        {
            return new DigSignGetResponse(){Success = false};
        }
    }

    /// <summary>
    /// Метод для хэширования пароля
    /// </summary>
    /// <param name="request"></param>
    private string HashMethod(DigSignGetRequest request)
    {
        hashedPassword = ComputeSha256Hash(request.Password);

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        return hashedPassword;
    }
}