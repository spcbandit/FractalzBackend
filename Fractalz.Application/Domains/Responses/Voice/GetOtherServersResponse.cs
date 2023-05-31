using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Voice;

namespace Fractalz.Application.Domains.Responses.Voice
{
    public class GetOtherServersResponse: BasicResponse
    {
        public List<VoiceServer> Servers { get; set; }
    }
}