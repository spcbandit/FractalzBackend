using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Infrastructure.EmailService;
using Xunit;


namespace EmailServiceTest
{       
        
    public class EmailServiceTest
    {
        [Fact]
        public void Test1()
        {
            var _EmailService = new EmailService();
            var res = _EmailService.SendEmail("aitovkarim@yandex.ru", "test", "test");
            
        }
    }
}