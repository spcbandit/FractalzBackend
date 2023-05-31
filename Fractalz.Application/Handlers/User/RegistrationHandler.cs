using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.Entities.Todo;
using Task = System.Threading.Tasks.Task;

namespace Fractalz.Application.Handlers.User
{
    /// <summary>
    /// 
    /// </summary>
    public class RegistrationHandler : IRequestHandler<RegistrationRequest, RegistrationResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;

        public RegistrationHandler(IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
        }

        public string hashedPassword { get; set; }

        public char[] specialSymbols =
            { '!', '*', '/', '?', ',', '<', '>', '$', '~', '`', '#', '№', '+', '=', '(', ')', ':', ';', '|' };

        public Regex Regex = new Regex("^(?=.*[a-z]).{1,18}$");


        public async Task<RegistrationResponse> Handle(RegistrationRequest request, CancellationToken cancellationToken)
        {
            //Валидация Email на содержание @
            if (!request.Email.Contains("@"))
                return new RegistrationResponse()
                    { Success = false, Message = MessageResource.User_emailValidation_At_Sign_False };
            /////////////////////////////////////////

            //Валидация на специальные символы
            if (request.Email.IndexOfAny(specialSymbols) != -1)
                return new RegistrationResponse()
                    { Success = false, Message = MessageResource.User_emailSpecialSymbolValidationFalse };
            /////////////////////////////////////////

            //Валидация на максимальную длинну пароля
            if (request.Password.ToArray().GetLength(0) > 18)
                return new RegistrationResponse()
                    { Success = false, Message = MessageResource.User_passwordMaxLenghtValidationFalse };
            /////////////////////////////////////////

            //Валидация на минимальную длинну пароля
            if (!(request.Password.ToArray().GetLength(0) > 6))
                return new RegistrationResponse()
                    { Success = false, Message = MessageResource.User_passwordMinLenghtValidationFalse };
            /////////////////////////////////////////

            //Валидация на прописную букву
            if (!Regex.IsMatch(request.Password))
                return new RegistrationResponse()
                    { Success = false, Message = MessageResource.User_passwordRegularSymbolsValidationFalse };
            /////////////////////////////////////////

            //Валидация на заглавную букву в пароле
            if (!request.Password.Any(x => char.IsUpper(x)))
                return new RegistrationResponse()
                    { Success = false, Message = MessageResource.User_passwordIsUpperCharValidationFalse };
            /////////////////////////////////////////

            //Проверка на существование логина
            if (!(_repositoryUser.Get(x => x.Login == request.Login).FirstOrDefault() == null))
                return new RegistrationResponse() { Success = false, Message = MessageResource.User_loginExist };
            /////////////////////////////////////////

            //Проверка на существование Email
            if (!(_repositoryUser.Get(x => x.Email == request.Email).FirstOrDefault() == null))
                return new RegistrationResponse() { Success = false, Message = MessageResource.User_emailExist };
            /////////////////////////////////////////

            var result = _repositoryUser.Create(new Domains.Entities.Profile.User()
            {
                Create = DateTime.Now,
                Email = request.Email,
                Login = request.Login,
                Password = HashMethod(request),
                Logo = new UserLogo(),
                TodoList = new TodoList(),
                Dialogs = new List<Dialog>()
            });

            if (result != 0)
            {
                return new RegistrationResponse() { Success = true };
            }
            else
            {
                return new RegistrationResponse() { Success = false, Message = MessageResource.ServerFailed };
            }
        }

        /// <summary>
        /// Метод для хэширования пароля
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        private string HashMethod(RegistrationRequest request)
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
    }
}
