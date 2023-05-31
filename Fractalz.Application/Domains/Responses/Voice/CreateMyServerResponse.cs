using Fractalz.Application.Domains.Entities.Voice;

namespace Fractalz.Application.Domains.Responses.Voice
{
    public class CreateMyServerResponse : BasicResponse
    {
        public VoiceServer Server { get; set; }
    }
}