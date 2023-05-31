using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fractalz.Application.Domains.Entities.Profile;

namespace Fractalz.Application.Domains.Entities.Chat
{
    public class Dialog
    {
        public Guid Id { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public string LastMessage { get; set; }
        public string DateSend { get; set; } 
        public List<User> Users { get; set; } = new List<User>();
        public DateTime Created { get; set; }
    }
}
