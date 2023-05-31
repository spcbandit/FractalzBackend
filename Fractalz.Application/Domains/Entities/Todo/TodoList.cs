using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Entities.Todo
{
    public class TodoList
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public Domains.Entities.Profile.User User { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public DateTime Created { get; set; }
    }
}
