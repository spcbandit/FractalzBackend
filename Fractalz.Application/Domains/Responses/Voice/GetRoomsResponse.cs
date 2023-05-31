using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Voice;

namespace Fractalz.Application.Domains.Responses.Voice
{
    public class GetRoomsResponse: BasicResponse
    {
        public List<VoiceRoom> Rooms { get; set; }
    }
}