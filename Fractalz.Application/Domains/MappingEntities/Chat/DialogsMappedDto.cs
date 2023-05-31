using System;
using System.Collections.Generic;
using Fractalz.Application.Domains.Entities.Profile;
using Newtonsoft.Json;

namespace Fractalz.Application.Domains.MappingEntities.Chat
{
    public class DialogsMappedDto
    {
        /// <summary>
        /// ИД - сообщения
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        /// <summary>
        /// ИД - отправителя
        /// </summary>
        [JsonProperty("userId")]
        public Guid UserId { get; set; }
        
        /// <summary>
        /// ИД - отправителя
        /// </summary>
        [JsonProperty("lastMessage")]
        public string LastMessage { get; set; }
        
        /// <summary>
        /// ИД - отправителя
        /// </summary>
        [JsonProperty("users")]
        public List<User> Users { get; set; } = new List<User>();
        
        /// <summary>
        /// ИД - отправителя
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        
        /// <summary>
        /// ИД - отправителя
        /// </summary>
        [JsonProperty("dateSend")]
        public string DateSend { get; set; }
        
        /// <summary>
        /// ИД - отправителя
        /// </summary>
        [JsonProperty("countUnReadMessage")]
        public int CountUnReadMessage { get; set; }
    }
}