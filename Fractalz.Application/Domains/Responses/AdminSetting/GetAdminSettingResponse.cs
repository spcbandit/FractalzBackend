using System.Collections.Generic;

namespace Fractalz.Application.Domains.Responses.AdminSetting;

public class GetAdminSettingResponse : BasicResponse
{
    public List<Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting> ListAdminSetting { get; set; }
}