using Newtonsoft.Json;

namespace Fractalz.Application.Domains.Entities.Websocket
{
    public class BasicWsEntities
    {
        [JsonProperty("type")]
        public WsMessageType Type { get; set; }

        [JsonProperty("data")]
        public object? Data { get; set; }
    }
}