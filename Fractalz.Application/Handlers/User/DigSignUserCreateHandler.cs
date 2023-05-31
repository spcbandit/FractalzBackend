using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.Entities.Todo;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using MediatR;
using Newtonsoft.Json;
using File = Fractalz.Application.Domains.Entities.Chat.File;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fractalz.Application.Handlers.User;

public class DigSignUserCreateHandler : IRequestHandler<DigSignUserCreateRequest, DigSignUserCreateResponse>
{
    private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
    public DigSignUserCreateHandler(IRepository<Domains.Entities.Profile.User> repositoryUser)
    {
        _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
    }
    
    private string hashedPassword { get; set; }
    private bool Valid { get; set; }

    private char[] specialSymbols =
        { '!', '*', '/', '?', ',', '<', '>', '$', '~', '`', '#', '№', '+', '=', '(', ')', ':', ';', '|' };

    private Regex Regex = new Regex("^(?=.*[a-z]).{1,18}$");

    public async Task<DigSignUserCreateResponse> Handle(DigSignUserCreateRequest request, CancellationToken cancellationToken)
    {
        
        if (CheckValidation(request))
        {
            var result = _repositoryUser.Create(new Domains.Entities.Profile.User()
            {
                Create = DateTime.Now,
                Email = request.Email,
                Login = request.Login,
                Password = HashMethod(request),
                Name = request.Name,
                Surname = request.Surname,
                Number = request.Number,
                Logo = new UserLogo(),
                IsEmailConfirmed = 1,
                TodoList = new TodoList(),
                Dialogs = new List<Dialog>()
            });

            if (result != 0)
            {
                return FileCreate(request);
            }
            
            else
            {
                return new DigSignUserCreateResponse() { Success = false, Message = MessageResource.ServerFailed };
            }
            
        }

        return new DigSignUserCreateResponse() { Success = false};

    }

    private DigSignUserCreateResponse FileCreate(DigSignUserCreateRequest request)
    {
        string UserSignsRepos = Environment.CurrentDirectory + $"/Digital sign users repository";

        if (!Directory.Exists(UserSignsRepos))
        {
            Directory.CreateDirectory(UserSignsRepos);
        }

        string infToSend = JsonSerializer.Serialize(request);
        
        using (FileStream fstream = new FileStream(Environment.CurrentDirectory + $"/Digital sign users repository/{request.OrganizationName}", FileMode.Create))
        {
            byte[] buffer = Encoding.Default.GetBytes(infToSend);
            fstream.WriteAsync(buffer, 0, buffer.Length);
        }
        byte[] fileBytes = System.IO.File.ReadAllBytes(Environment.CurrentDirectory + $"/Digital sign users repository/{request.OrganizationName}");
            
        var memoryStream = new MemoryStream(fileBytes);

        if (memoryStream.Length != 0)
        {
            return new DigSignUserCreateResponse() 
            {
                Success = true, 
                FileStream = new Microsoft.AspNetCore.Mvc.FileStreamResult(memoryStream, "application/octet-stream"){FileDownloadName = request.OrganizationName}
            };
            
        }
        else
        {
            return new DigSignUserCreateResponse(){Success = false};
        }
    }

    /// <summary>
    /// Метод для хэширования пароля
    /// </summary>
    /// <param name="request"></param>
    private string HashMethod(DigSignUserCreateRequest request)
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

        async Task<string> WriteRegistrationInfo()
        {
            System.IO.File.Create(@"C:\\Users\Admin\Desktop\test.txt");
            var path = "C:/Users/Admin/Desktop/DigSign.txt";
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(request.Password);
            }

            using (StreamWriter writer = new StreamWriter(path, true))
            {
                await writer.WriteLineAsync("Addition");
                await writer.WriteAsync("4,5");
            }

            return null;
        }

        return hashedPassword;
    }

    public bool CheckValidation (DigSignUserCreateRequest request)
    {
        //Валидация Email на содержание @
        if (!request.Email.Contains("@"))
            return false;
        /////////////////////////////////////////

        //Валидация на специальные символы
        if (request.Email.IndexOfAny(specialSymbols) != -1)
            return false;
        /////////////////////////////////////////

        //Валидация на максимальную длинну пароля
        if (request.Password.ToArray().GetLength(0) > 18)
            return false;
        /////////////////////////////////////////

        //Валидация на минимальную длинну пароля
        if (!(request.Password.ToArray().GetLength(0) > 6))
            return false;
        /////////////////////////////////////////

        //Валидация на прописную букву
        if (!Regex.IsMatch(request.Password))
            return false;
        /////////////////////////////////////////

        //Валидация на заглавную букву в пароле
        if (!request.Password.Any(x => char.IsUpper(x)))
            return false;
        /////////////////////////////////////////

        //Проверка на существование логина
        if (!(_repositoryUser.Get(x => x.Login == request.Login).FirstOrDefault() == null))
            return false;
        /////////////////////////////////////////

        //Проверка на существование Email
        if (!(_repositoryUser.Get(x => x.Email == request.Email).FirstOrDefault() == null))
            return false;
        /////////////////////////////////////////
        return true;
    }
}
