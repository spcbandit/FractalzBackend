using System;
using Xunit;
using Fractalz.Infrastructure.EmailService;
using Fractalz.Application.Abstractions;


namespace EmailTest
{
    public class SendEmailTest
    {
        [Fact]
        public void SendMailTest()
        {
            
            // Arrange

            var sendEmail = new SendEmail();
            
            // Act 

            var send = SendEmail.Send();
            
            // Assert
            
            Assert.Equal("Success", result?.ViewData[post]);
            
        }

    }
}