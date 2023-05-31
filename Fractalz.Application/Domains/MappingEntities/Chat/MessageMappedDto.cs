using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Extentions;
using Newtonsoft.Json;

namespace Fractalz.Application.Domains.MappingEntities.Chat
{
    public class MessageMappedDto
    {
        /// <summary>
        /// ИД - сообщения
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }
     
        /// <summary>
        /// NameSender
        /// </summary>
        [JsonProperty("nameSender")]
        public string NameSender { get; set; }
        
        /// <summary>
        /// ИД - диалога
        /// </summary>
        [JsonProperty("dialogId")]
        public Guid DialogId { get; set; }

        // [ForeignKey(nameof(DialogId))]
        // public Dialog Dialog { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("created")]
        public DateTime Created { get; set; }
        public string _dateCreated { get; set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        [JsonProperty("dateCreated")]
        public string DateCreated {
            get
            {
                var _dateCreated = Created.ToBeautyTime();
                return _dateCreated;
            }
            set
            {
                _dateCreated = value;
            } 
        }

        /// <summary>
        /// ИД - отправителя
        /// </summary>
        [JsonProperty("idSender")]
        public Guid IdSender { get; set; }

        /// <summary>
        /// Прочитано
        /// </summary>
        [JsonProperty("isOnRead")]
        public bool IsOnRead { get; set; }

        /// <summary>
        /// Доставлено
        /// </summary>
        [JsonProperty("isDelivered")]
        public bool IsDelivered { get; set; }

        /// <summary>
        /// Изменено
        /// </summary>
        [JsonProperty("isUpdate")]
        public bool IsUpdate { get; set; }

        /// <summary>
        /// Список файлов
        /// </summary>
        [JsonProperty("file")]
        public List<FileMappedDto> File { get; set; } = new List<FileMappedDto>();

        /// <summary>
        /// Список реакций
        /// </summary>
        [JsonProperty("reactions")]
        public List<Reaction> Reactions { get; set; } = new List<Reaction>();
    }
}