using System;
using Fractalz.Application.Domains.Entities.Profile;

namespace Fractalz.Application.Domains.Entities.Chat
{
    public class DialogUser
    {
        public Guid UserId { get; set; }
        public Dialog Dialog { get; set; }
        public User User { get; set; }
        public Guid DialogId { get; set; }
    }
}