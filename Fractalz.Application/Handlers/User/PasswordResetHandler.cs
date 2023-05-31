using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Fractalz.Application.Handlers.User
{
    public class PasswordResetHandler: IRequestHandler<PasswordResetRequest,PasswordResetResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        public PasswordResetHandler(IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
        }
        
        public string hashedPassword { get; set; }
        public Regex Regex = new Regex("^(?=.*[a-z]).{1,18}$");
        public async Task<PasswordResetResponse> Handle(PasswordResetRequest request, CancellationToken cancellationToken)
        {
            //Валидация на максимальную длинну пароля
            if (request.Password.ToArray().GetLength(0) > 18)
                return new PasswordResetResponse() { Success = false, Message = MessageResource.User_passwordMaxLenghtValidationFalse };
            /////////////////////////////////////////
            
            //Валидация на минимальную длинну пароля
            if (!(request.Password.ToArray().GetLength(0) > 6))
                return new PasswordResetResponse() { Success = false, Message = MessageResource.User_passwordMinLenghtValidationFalse };
            /////////////////////////////////////////
            
            //Валидация на прописную букву
            if (!Regex.IsMatch(request.Password))
                return new PasswordResetResponse() { Success = false, Message = MessageResource.User_passwordRegularSymbolsValidationFalse};
            /////////////////////////////////////////
            
            //Валидация на заглавную букву в пароле
            if (!request.Password.Any(x => char.IsUpper(x)))
                return new PasswordResetResponse() { Success = false, Message = MessageResource.User_passwordIsUpperCharValidationFalse };
            /////////////////////////////////////////

            //Проверка на существование Email
            if (_repositoryUser.Get(x => x.Email == request.existEmail).FirstOrDefault() == null)
                return new PasswordResetResponse() { Success = false, Message = MessageResource.User_emailNotExist };
            /////////////////////////////////////////
            var email = _repositoryUser.GetWithInclude(x => x.Email == request.existEmail).FirstOrDefault();
            if(email != null)
            {
                email.Password = HashMethod(request);
                var reset = _repositoryUser.Update(email);
                return new PasswordResetResponse() { Success = true };

            }
            else
            { return new PasswordResetResponse() { Success = false, Message = MessageResource.ServerFailed }; } 
        }
        /// <summary>
        /// Метод для хэширования пароля
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        private string HashMethod(PasswordResetRequest request)
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
}