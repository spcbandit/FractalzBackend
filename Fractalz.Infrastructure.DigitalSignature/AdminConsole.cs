using System;
using Fractalz.Application.Abstractions;

namespace Fractalz.Infrastructure.DigitalSignature
{
    
    public class AdminConsole
    {
        private UserInformation userInformation;
        public AdminConsole(UserInformation inf)
        {
            userInformation = inf;
        }

        public void Cons()
        {
            Console.WriteLine("Создание нового пользователя в системе Fractalz");
            
            Console.WriteLine("Введите Имя пользователя: ");
            userInformation.name = Console.ReadLine();
            
            Console.WriteLine("Введите Фамилию пользователя: ");
            userInformation.surname = Console.ReadLine();
            
            Console.WriteLine("Введите Login пользователя: ");
            userInformation.login = Console.ReadLine();
            
            Console.WriteLine("Введите Email пользователя: ");
            userInformation.email = Console.ReadLine();
            
            Console.WriteLine("Введите Password пользователя: ");
            userInformation.password = Console.ReadLine();
            
            Console.WriteLine("Введите Номер пользователя: ");
            userInformation.number = Console.ReadLine();
        }

        public bool InfoCheck()
        {
            if (userInformation.name == null 
                || userInformation.surname == null 
                || userInformation.login == null 
                || userInformation.email == null 
                || userInformation.number == null 
                || userInformation.password == null) 
                return false;
            UserCreate(userInformation);
            return true;
            
            
        }

        public bool UserCreate(UserInformation information)
        {
            UserCreateDB userCreateDb = new UserCreateDB(information);
            return true;
        }

    }
}