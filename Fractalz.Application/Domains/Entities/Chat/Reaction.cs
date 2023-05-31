using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Entities.Chat
{
    public record Reaction
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Domains.Entities.Profile.User User { get; set; }
        public EmojiType EmojiType { get; set; }
        public DateTime DateTime { get; set; }
        public Guid MessageId { get; set; }
    }
}