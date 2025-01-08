using Application.Common.Mapping;
using AutoMapper;

namespace Application.CQRS.Member.Queries.GetGroupMembersQuery;

public class MemberLookupDto : IMapWith<Domain.Entities.Member>
{
    public Guid Id { get; set; }
    public string Nickname { get; set; }
    public DateTimeOffset JoinDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Member, MemberLookupDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
            .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.CreatedAt));
    }
}