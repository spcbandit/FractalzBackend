using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Fractalz.Application.Domains.Entities.Voice
{
    public record VoiceRoom
    {
        //[Key]
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public DateTime Updated { get; set; } = DateTime.Now;
        public virtual List<Fractalz.Application.Domains.Entities.Profile.User> Users { get; set; } = new List<Fractalz.Application.Domains.Entities.Profile.User>();
    }
}