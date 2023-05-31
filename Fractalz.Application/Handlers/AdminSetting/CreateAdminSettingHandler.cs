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

public class CreateAdminSettingHandler : IRequestHandler<CreateAdminSettingRequest, CreateAdminSettingResponse>
{
    private readonly IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> _repositoryAdminSetting;

    public CreateAdminSettingHandler(IRepository<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> repositoryAdminSetting)
    {
        _repositoryAdminSetting = repositoryAdminSetting ?? throw new ArgumentNullException(nameof(repositoryAdminSetting));;
    }

    public async Task<CreateAdminSettingResponse> Handle(CreateAdminSettingRequest request,
        CancellationToken cancellationToken)
    {
        var listAdmin = new Domains.Entities.AdminSettings.AdminSetting();
            listAdmin.Date = request.Date;
            listAdmin.Host = request.Host;
            listAdmin.Name = request.Name;
            listAdmin.Port = request.Port;
            listAdmin.IsActive = request.IsActive;
            listAdmin.IsDeleted = request.IsDeleted;

            var resp = _repositoryAdminSetting.Create(listAdmin);
            
        if (resp != 0)
        {
            return new CreateAdminSettingResponse() { Success = true, AdminSettingId = listAdmin.Id};
        }
        else
        {
            return new CreateAdminSettingResponse() { Success = false, Message = "AdminSetting not create" };
        }
    }
}
