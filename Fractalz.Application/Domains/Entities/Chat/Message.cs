using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Entities.Chat
{
    /// <summary>
    /// Сообщение
    /// </summary>
    public class Message
    {
        /// <summary>
        /// ИД - сообщения
        /// </summary>
        public Guid Id { get; set; }
     
        /// <summary>
        /// ИД - диалога
        /// </summary>
        public Guid DialogId { get; set; }

        // [ForeignKey(nameof(DialogId))]
        // public Dialog Dialog { get; set; }

        /// <summary>
        /// Текст сообщения
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// ИД - отправителя
        /// </summary>
        public Guid IdSender { get; set; }

        /// <summary>
        /// Прочитано
        /// </summary>
        public bool IsOnRead { get; set; }

        /// <summary>
        /// Доставлено
        /// </summary>
        public bool IsDelivered { get; set; }

        /// <summary>
        /// Изменено
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// Список файлов
        /// </summary>
        public List<File> File { get; set; } = new List<File>();

        /// <summary>
        /// Список реакций
        /// </summary>
        public List<Reaction> Reactions { get; set; } = new List<Reaction>();
    }
}
