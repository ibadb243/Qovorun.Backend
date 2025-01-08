using Application.Common.Mapping;
using AutoMapper;

namespace Application.CQRS.User.Queries.GetDetailsQuery;

public class UserDetailsVm : IMapWith<Domain.Entities.User>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Shortname { get; set; }
    public string Description { get; set; }
    
    public void Mapping(Profile profile) => profile.CreateMap<Domain.Entities.User, UserDetailsVm>()
        .ForMember(dest => dest.Firstname, opt =>
            opt.MapFrom(src => src.Firstname))
        .ForMember(dest => dest.Lastname, opt =>
            opt.MapFrom(src => src.Lastname))
        .ForMember(dest => dest.Description, opt =>
            opt.MapFrom(src => src.Description));
}