using System.Collections.Generic;

namespace Fractalz.Application.Domains.Responses.Voice
{
    public class GetUsersRoomResponse: BasicResponse
    {
        public List<Entities.Profile.User> Users { get; set; }
    }
}