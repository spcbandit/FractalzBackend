using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Todo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Fractalz.Application.Domains.Entities.Conference;
using Fractalz.Application.Domains.Entities.Documents;
using Fractalz.Application.Domains.Entities.Voice;
using Newtonsoft.Json;

namespace Fractalz.Application.Domains.Entities.Profile

{
    public record User
    {
        /// <summary>
        /// 
        /// </summary>
        //[Key]
        public Guid Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Status OnlineStatus { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Patro { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AuthCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Create { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastLogin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IsEmailConfirmed { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public UserLogo Logo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonIgnore]
        public List<Dialog> Dialogs { get; set; } = new List<Dialog>();

        /// <summary>
        /// 
        /// </summary>
        public TodoList TodoList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<VoiceServer> MyVoiceServer { get; set; } = new List<VoiceServer>();

        /// <summary>
        /// 
        /// </summary>
        // public List<VoiceServer> OtherVoiceServers { get; set; } = new List<VoiceServer>();

        /// <summary>
        /// 
        /// </summary>
        public Guid? VoiceRoomId { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public virtual VoiceRoom VoiceRoom  { get; set; }
    }
}
