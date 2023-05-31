using System;
using System.Collections.Generic;

namespace Fractalz.Application.Domains.Responses.AdminSetting;

public class CreateAdminSettingResponse : BasicResponse
{
    public Guid AdminSettingId { get; set; }
}