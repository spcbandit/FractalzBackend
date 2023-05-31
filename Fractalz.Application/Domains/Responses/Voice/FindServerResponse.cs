using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Voice;

namespace Fractalz.Application.Domains.Responses.Voice
{
    public class FindServerResponse: BasicResponse
    {
        public VoiceServer Servers { get; set; }
    }
}