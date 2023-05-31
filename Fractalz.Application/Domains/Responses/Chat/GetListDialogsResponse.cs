using Fractalz.Application.Domains.Entities.Chat;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractalz.Application.Domains.MappingEntities.Chat;

namespace Fractalz.Application.Domains.Responses.Chat
{
    public class GetListDialogsResponse : BasicResponse
    {
        public List<DialogsMappedDto> Dialogs { get; set; }
    }
}
