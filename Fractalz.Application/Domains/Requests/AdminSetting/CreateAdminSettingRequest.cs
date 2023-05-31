using System;
using Fractalz.Application.Domains.Responses.AdminSetting;
using MediatR;

namespace Fractalz.Application.Domains.Requests.AdminSetting;

public class CreateAdminSettingRequest : IRequest<CreateAdminSettingResponse>
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public bool IsActive { get; set; }
    public bool IsDeleted { get; set; }
}