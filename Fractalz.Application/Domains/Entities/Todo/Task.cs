using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fractalz.Application.Domains.Entities.Todo
{
    public class Task
    {
        public Guid Id { get; set; }

        public Guid TodoListId { get; set; }

        [ForeignKey(nameof(TodoListId))]
        [JsonIgnore]
        public TodoList TodoList { get; set; }

        public string Header { get; set; }

        public string About { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime DateCreate { get; set; }

        public int DurationInMinute { get; set; }

        public bool IsCompleted { get; set; }
    }
}
