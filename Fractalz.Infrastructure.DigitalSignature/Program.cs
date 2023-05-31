using System;
using Fractalz.Infrastructure.Database.Contexts;
using Fractalz.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Fractalz.Infrastructure.DigitalSignature
{
    class Program
    {
       
        static void Main(string[] args)
        {
            AdminConsole adminConsole = new AdminConsole(inf:new UserInformation());
            adminConsole.Cons();
            
            if (adminConsole.InfoCheck())
            {
                
            }
            else
            {
                Console.WriteLine("FAIL");
            }
        }

    }
    
}