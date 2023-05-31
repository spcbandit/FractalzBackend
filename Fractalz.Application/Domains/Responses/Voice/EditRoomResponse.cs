using Fractalz.Application.Domains.Entities.Voice;

namespace Fractalz.Application.Domains.Responses.Voice
{
    public class EditRoomResponse: BasicResponse
    {
        public VoiceRoom Room { get; set; }
    }
}