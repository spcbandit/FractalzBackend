using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fractalz.Application.Domains.Entities.Profile;

namespace Fractalz.Application.Domains.Entities.Conference
{
    public record ConferenceEntity 
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [ForeignKey(nameof(Token))]
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? DayLoop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Updated { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DateTimeStart { get; set; }

    }
}