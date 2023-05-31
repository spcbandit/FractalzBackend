using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fractalz.Application.Domains.Entities.Voice
{
    public record VoiceServer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime Updated { get; set; } = DateTime.Now;

        [ForeignKey("UserId")] public Guid UserId { get; set; }

        public List<VoiceRoom> Rooms { get; set; } = new List<VoiceRoom>();
    }
}