using System;
using Fractalz.Application.Domains.Entities.Profile;

namespace Fractalz.Application.Domains.MappingEntities.Chat
{
    public class FindUserMappedDto
    {
        public Guid Id { get; set; }
        
        public Status OnlineStatus { get; set; }
        
        public string Name { get; set; }

        public string LastMessage { get; set; } = "Кажется вы не обменивались сообщениями";
        public int CountUnReadMessage { get; set; } = 0;
    }
}