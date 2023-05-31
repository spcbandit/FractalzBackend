using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fractalz.Application.Abstractions;
using Fractalz.Application.Domains.Requests.AdminSetting;
using Fractalz.Application.Domains.Responses.AdminSetting;
using MediatR;

namespace Fractalz.Application.Handlers.AdminSetting;

public class UpdateAdminSettingHandler : IRequestHandler<UpdateAdminSettingRequest, UpdateAdminSettingResponse>
{
    private readonly IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> _repositoryAdminSetting;

    public UpdateAdminSettingHandler(IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> repositoryAdminSetting)
    {
        _repositoryAdminSetting = repositoryAdminSetting ?? throw new ArgumentNullException(nameof(repositoryAdminSetting));;
    }
    
    public async Task<UpdateAdminSettingResponse> Handle(UpdateAdminSettingRequest request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        { return new UpdateAdminSettingResponse() { Success = false, Message = "Id AdminSetting can not be 0" }; }
         
         var listAdmin = _repositoryAdminSetting.Get(i=>i.Id == request.Id).FirstOrDefault();
         listAdmin.Id = request.Id;
         listAdmin.Date = request.Date;
         listAdmin.Host = request.Host;
         listAdmin.Name = request.Name;
         listAdmin.Port = request.Port;
         listAdmin.IsActive = request.IsActive;
         listAdmin.IsDeleted = request.IsDeleted;

         var resp = _repositoryAdminSetting.Update(listAdmin);
        
         if (resp != 0)
         { return new UpdateAdminSettingResponse() { Success = true, ListAdminSetting = listAdmin}; }
         else
         { return new UpdateAdminSettingResponse() { Success = false, Message = "AdminSetting not update" }; }
    }
}