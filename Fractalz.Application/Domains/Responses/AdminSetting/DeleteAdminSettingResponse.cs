using System;
using System.Collections.Generic;

namespace Fractalz.Application.Domains.Responses.AdminSetting
{
    public class DeleteAdminSettingResponse : BasicResponse
    {
        public Guid AdminSettingId { get; set; }
    }
}