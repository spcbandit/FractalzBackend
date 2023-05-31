using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.MappingEntities.Chat;
using Fractalz.Application.Domains.Requests.Chat;
using Fractalz.Application.Domains.Responses.Chat;
using MediatR;

namespace Fractalz.Application.Handlers.Chat
{
    public class FindUserHandler : IRequestHandler<FindUserRequest, FindUserResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        private readonly IMapper _mapper;
        public FindUserHandler(IRepository<Domains.Entities.Profile.User> repositoryUser, IMapper mapper)
        {
            _repositoryUser = repositoryUser;
            _mapper = mapper;
        }

        public async Task<FindUserResponse> Handle(FindUserRequest request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.FindStr))
            {
                return new FindUserResponse() {Success = false, Message = "Value can not be empty"};
            }

            var users = _repositoryUser.Get(x => x.Email == request.FindStr
                                                 || x.Login == request.FindStr
                                                 || x.Name == request.FindStr
                                                 || x.Surname == request.FindStr
                                                 || x.Patro == request.FindStr).ToList();

            foreach (var user in users)
            {
                if (user.Name == null)
                {
                    user.Name = user.Login;
                }
                else
                {
                    user.Name = $"{user.Patro} {user.Name} {user.Surname}";
                }
            }
            
            var res = _mapper.Map<List<FindUserMappedDto>>(users);
            
            if(users.Count != 0)
                return new FindUserResponse() {Success = true, Users = res};
            else
                return new FindUserResponse() {Success = false, Message = "Пользователь не найден"};
        }
    }
}