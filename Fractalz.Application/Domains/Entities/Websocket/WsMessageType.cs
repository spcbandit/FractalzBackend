using System.Runtime.Serialization;

namespace Fractalz.Application.Domains.Entities.Websocket
{
    public enum WsMessageType
    {
        [EnumMember(Value="User")]
        User,
        [EnumMember(Value="Dialog")]
        Dialog,
        [EnumMember(Value="Message")]
        Message,
        [EnumMember(Value="Voice")]
        Voice,
        [EnumMember(Value="Noty")]
        Noty
    }
}