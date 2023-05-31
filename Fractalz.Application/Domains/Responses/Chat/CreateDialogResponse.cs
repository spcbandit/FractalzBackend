using Fractalz.Application.Domains.Entities.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Responses.Chat
{
    public class CreateDialogResponse : BasicResponse
    {
        public Dialog Dialog { get; set; }
    }
}
