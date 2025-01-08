using Application.Common.Mapping;
using AutoMapper;

namespace Application.CQRS.Member.Queries.GetUserParticipationsQuery;

public class UserParticipantDto : IMapWith<MemberGroupDto>
{
    public Guid GroupId { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<MemberGroupDto, UserParticipantDto>()
            .ForMember(dest => dest.GroupId, opt => opt.MapFrom(src => src.Member.GroupId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Group.Name));
    }
}