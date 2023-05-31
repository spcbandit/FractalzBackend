using System.Collections.Generic;

namespace Fractalz.Application.Domains.Responses.AdminSetting;

public class UpdateAdminSettingResponse : BasicResponse
{
    public  Fractalz.Application.Domains.Entities.AdminSettings.AdminSetting ListAdminSetting{ get; set; }
}