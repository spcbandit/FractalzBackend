using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.MappingEntities.Chat;

namespace Fractalz.Application.Domains.Responses.Chat
{
    public class FindUserResponse: BasicResponse
    {
        public List<FindUserMappedDto> Users { get; set; }
    }
}