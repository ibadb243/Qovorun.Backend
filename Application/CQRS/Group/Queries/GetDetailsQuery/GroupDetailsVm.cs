using Application.Common.Mapping;
using AutoMapper;

namespace Application.CQRS.Group.Queries.GetDetailsQuery;

public class GroupDetailsVm : IMapWith<Domain.Entities.Group>
{
    public string Name { get; set; }
    public string Shortname { get; set; }
    public string Description { get; set; }

    public void Mapping(Profile profile) => profile.CreateMap<Domain.Entities.Group, GroupDetailsVm>()
        .ForMember(dest => dest.Name, opt => 
            opt.MapFrom(src => src.Name))
        .ForMember(dest => dest.Description, opt =>
            opt.MapFrom(src => src.Description));
}