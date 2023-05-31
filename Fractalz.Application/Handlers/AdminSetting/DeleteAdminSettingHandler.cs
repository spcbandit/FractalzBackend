using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.AdminSetting;
using Fractalz.Application.Domains.Responses.AdminSetting;
using MediatR;

namespace Fractalz.Application.Handlers.AdminSetting
{
    public class DeleteAdminSettingHandler : IRequestHandler<DeleteAdminSettingRequest, DeleteAdminSettingResponse>
    {
        private readonly IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> _repositoryAdminSetting;

        public DeleteAdminSettingHandler(IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> repositoryAdminSetting)
        {
            _repositoryAdminSetting = repositoryAdminSetting?? throw new ArgumentNullException(nameof(repositoryAdminSetting));;
        }

        public async Task<DeleteAdminSettingResponse> Handle(DeleteAdminSettingRequest request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty)
            { return new DeleteAdminSettingResponse() { Success = false, Message = "Id Message can not be 0" }; }
            
            var countDelete = _repositoryAdminSetting.Get(i=>i.Id == request.Id).FirstOrDefault();
            var resp = _repositoryAdminSetting.Remove(countDelete);
            
            if (resp != 0)
            { return new DeleteAdminSettingResponse() { Success = true, AdminSettingId = countDelete.Id}; }
            else
            { return new DeleteAdminSettingResponse() { Success = false, Message = "AdminSetting not delete" }; }
        }
    }
}