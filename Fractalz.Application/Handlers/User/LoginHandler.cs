using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;

using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace Fractalz.Application.Handlers.User
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        public string hashedPassword { get; set; }
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly IConfiguration _configuration;
        public LoginHandler(IRepository<Domains.Entities.Profile.User> repositoryUser, IConfiguration configuration)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
            _configuration = configuration;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
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
            /*Вход для ЭЦП*/
            if (request.Password.Length > 18)
            {
                var user = _repositoryUser.GetWithInclude(x => (x.Login == request.Login || x.Email == request.Login) && 
                x.Password == request.Password, user =>user.TodoList.Tasks).FirstOrDefault();
                if (user == null)
                { return new LoginResponse() { Message = MessageResource.User_passwordWron, Success = false,Token=null }; }
                if (user.IsEmailConfirmed == 0)
                { return new LoginResponse() { Message = MessageResource.User_codeValidWron, Success = false,Token=null }; }
                else
                { return new LoginResponse() { Success = true, User = user,Token= GenerateJwtToken(user.Id.ToString()) }; }
            }
            
            if (_repositoryUser.Get(x => x.Login == request.Login || request.Login == x.Email).FirstOrDefault() != null)
            {
                var user = _repositoryUser.GetWithInclude(x => (x.Login == request.Login || x.Email == request.Login) && 
                x.Password == hashedPassword, user =>user.TodoList.Tasks)
                    .FirstOrDefault();

                if (user == null)
                { return new LoginResponse() { Message = MessageResource.User_passwordWron, Success = false,Token=null }; }
                if (user.IsEmailConfirmed == 0)
                { return new LoginResponse() { Message = MessageResource.User_codeValidWron, Success = false,Token=null }; }
                else
                { return new LoginResponse() { Success = true, User = user,Token= GenerateJwtToken(user.Id.ToString()) }; }
            }
            else
            { return new LoginResponse() { Message = MessageResource.User_loginWron, Success = false }; }
        }

        #region GenerateToken
        /// <summary>
        /// Generate JWT Token
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
                Expires = DateTime.UtcNow.AddHours(24),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        #endregion
    }
}
