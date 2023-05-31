using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.AdminSetting;
using Fractalz.Application.Domains.Responses.AdminSetting;
using MediatR;

namespace Fractalz.Application.Handlers.AdminSetting
{
    public class GetAdminSettingHandler : IRequestHandler<GetAdminSettingRequest, GetAdminSettingResponse>
    {
        private readonly IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> _repositoryAdminSetting;

        public GetAdminSettingHandler(IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> repositoryAdminSetting)
        {
            _repositoryAdminSetting = repositoryAdminSetting?? throw new ArgumentNullException(nameof(repositoryAdminSetting));;
        }
        
        public async Task<GetAdminSettingResponse> Handle(GetAdminSettingRequest request, CancellationToken cancellationToken)
        {
            //all rows
            var listAdmin = _repositoryAdminSetting.Get().ToList();
            return new GetAdminSettingResponse(){Success = true, ListAdminSetting = listAdmin};
        }
    }
}