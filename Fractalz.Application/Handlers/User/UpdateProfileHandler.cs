using Fractalz.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractalz.Application.Domains.Entities.Profile;
using System.Threading;
using Fractalz.Application.Domains.Requests.User;
using Fractalz.Application.Domains.Responses.User;
using System.IO;
using MediatR;

namespace Fractalz.Application.Handlers.User
{
    public class UpdateProfileHandler: IRequestHandler<UpdateProfileRequest, UpdateProfileResponse>
    {
        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;
        public UpdateProfileHandler(IRepository<Domains.Entities.Profile.User> repositoryUser)
        {
            _repositoryUser = repositoryUser ?? throw new ArgumentNullException(nameof(repositoryUser));
        }

        public async Task<UpdateProfileResponse> Handle(UpdateProfileRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            { return new UpdateProfileResponse() { Success = false, Message = "User Id can not be 0" }; }

            var user = _repositoryUser.GetWithInclude(x => x.Id == request.UserId, x => x.Logo).FirstOrDefault();

            if (user != null)
            {
                user.Email = request.Email;
                user.Login = request.Login;
                user.Name = request.Name;
                user.Number = request.Number;
                user.Patro = request.Patro;
                user.Surname = request.Surname;
                var result = _repositoryUser.Update(user);

                if (request.Logo != null)
                {
                    var uploadPath = $"{Environment.CurrentDirectory}/users/{request.UserId}";

                    var path = uploadPath + request.Logo.FileName;

                    using (var fileStream = new FileStream(path, FileMode.Create))
                        await request.Logo.CopyToAsync(fileStream);

                    user.Logo = new UserLogo
                    {
                        ByteLength = request.Logo.Length,
                        Extension = Path.GetExtension(request.Logo.FileName),
                        Path = path,
                        FileName = request.Logo.FileName
                    };
                }

                if (result != 0)
                { return new UpdateProfileResponse() { Success = true }; }
                else 
                { return new UpdateProfileResponse() { Success = false, Message = "User not updated" }; }

            }
            else { return new UpdateProfileResponse() { Success = false, Message = "User not found" }; }

        }

    }
}
