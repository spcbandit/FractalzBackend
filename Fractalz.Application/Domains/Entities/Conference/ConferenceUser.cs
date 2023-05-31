using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fractalz.Application.Domains.Entities.Profile;

namespace Fractalz.Application.Domains.Entities.Conference
{
    public record ConferenceUser 
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ConferencesId { get; set; }

        public Guid? UsersId { get; set; }

        public bool IsTemporary { get; set; }
        public bool IsAdmin { get; set; }
    }
}