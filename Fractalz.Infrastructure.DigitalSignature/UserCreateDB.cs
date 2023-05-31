using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Infrastructure.Database.Contexts;
using Fractalz.Infrastructure.Database.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Fractalz.Infrastructure.DigitalSignature
{
    public class UserCreateDB
    {
        private RestClient _client;
        private UserInformation _information;

        public UserCreateDB(UserInformation information)
        {
            _information = information;
            UserCreate();
        }

        public async void UserCreate()
        {
            _client = new RestClient("http://localhost:5001");
            var request = new RestRequest("user/digSignUserCreate", Method.Post);
            string infToSend = JsonSerializer.Serialize(_information);
            request.AddParameter("application/json", infToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response =  await _client.PostAsync(request);
            Console.WriteLine(response);

        }
    }
}
