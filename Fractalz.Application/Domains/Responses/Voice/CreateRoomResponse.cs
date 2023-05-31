using Fractalz.Application.Domains.Entities.Voice;

namespace Fractalz.Application.Domains.Responses.Voice
{
    public class CreateRoomResponse: BasicResponse
    {
        public VoiceRoom Room { get; set; }
    }
}