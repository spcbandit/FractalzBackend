using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Entities.Voice;
using Fractalz.Application.Domains.Requests.Voice;
using Fractalz.Application.Domains.Responses.Voice;
using MediatR;

namespace Fractalz.Application.Handlers.Voice
{
    public class CreateMyServerHandler : IRequestHandler<CreateMyServerRequest, CreateMyServerResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly IRepository<VoiceServer> _repositoryServer;

        /// <summary>
        /// CreateMyServerHandler
        /// </summary>
        /// <param name="repositoryUser"></param>
        /// <param name="repositoryServer"></param>
        /// <exception cref="ArgumentException"></exception>
        public CreateMyServerHandler(IRepository<Domains.Entities.Profile.User> repositoryUser, IRepository<VoiceServer> repositoryServer)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentException(nameof(repositoryUser));
            _repositoryServer = repositoryServer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CreateMyServerResponse> Handle(CreateMyServerRequest request, CancellationToken cancellationToken)
        {
            var user = _repositoryUser.Get(x => x.Id == request.UserId).FirstOrDefault();
            var server = new VoiceServer()
            {
                Name = request.NameServer
            };
            var resultServ = _repositoryServer.Create(server);
            if (resultServ != 0)
            {
                user.MyVoiceServer.Add(server);
                var result = _repositoryUser.Update(user);
                if (result != 0)
                {
                    return new CreateMyServerResponse() {Success = true, Server = user.MyVoiceServer.FirstOrDefault()};
                }
                else
                {
                    return new CreateMyServerResponse() {Success = false, Message = "Cannot create server"};
                }
            }
            else
            {
                return new CreateMyServerResponse() {Success = false, Message = "Cannot create server"};
            }
        }
    }
}