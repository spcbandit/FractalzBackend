using AutoMapper;
using Fractalz.Application.Domains.Entities.Chat;
using Fractalz.Application.Domains.Entities.Profile;
using Fractalz.Application.Domains.MappingEntities.Chat;

namespace Fractalz.Application.Mapping
{
    public class MappingProfile : Profile {
        /// <summary>
        /// MappingProfile
        /// </summary>
        public MappingProfile() {
            CreateMap<User, FindUserMappedDto>();
            CreateMap<Dialog, DialogsMappedDto>();
            CreateMap<Message, MessageMappedDto>();
            CreateMap<File, FileMappedDto>();
        }
    }
}