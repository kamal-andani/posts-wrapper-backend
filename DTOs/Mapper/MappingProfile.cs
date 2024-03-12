// Auto Mapper for DTOs

using AutoMapper;
using zum_rails.DataObjects;

namespace zum_rails.DTOs.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        { 
            CreateMap<PostDetails, PostDetailsDto>();
        }
    }
}
