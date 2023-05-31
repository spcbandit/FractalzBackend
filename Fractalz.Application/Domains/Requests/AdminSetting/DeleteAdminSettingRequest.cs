using System;
using Fractalz.Application.Domains.Responses.AdminSetting;
using MediatR;

namespace Fractalz.Application.Domains.Requests.AdminSetting;
public class DeleteAdminSettingRequest : IRequest<DeleteAdminSettingResponse>
{
    public Guid Id { get; set; }
}