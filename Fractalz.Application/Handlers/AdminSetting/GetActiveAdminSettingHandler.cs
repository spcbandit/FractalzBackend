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
    public class GetActiveAdminSettingHandler : IRequestHandler<GetActiveAdminSettingRequest, GetActiveAdminSettingResponse>
    {
        private readonly IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> _repositoryAdminSetting;

        public GetActiveAdminSettingHandler(IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> repositoryAdminSetting)
        {
            _repositoryAdminSetting = repositoryAdminSetting?? throw new ArgumentNullException(nameof(repositoryAdminSetting));;
        }
        
        public async Task<GetActiveAdminSettingResponse> Handle(GetActiveAdminSettingRequest request, CancellationToken cancellationToken)
        {
            //IsActive - true, IsDeleted - false, FirstOrDefault 
            var activeAdminSetting = _repositoryAdminSetting.Get(i => i.IsActive == true && i.IsDeleted == false).OrderByDescending((setting => setting.Date)).FirstOrDefault();
            return new GetActiveAdminSettingResponse(){Success = true, ListAdminSetting = activeAdminSetting};
        }

        
    }
}