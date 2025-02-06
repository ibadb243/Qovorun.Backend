using Application.Common.Mapping;
using Application.CQRS.User.Commands.CreateCommand;
using AutoMapper;
using Domain;

namespace WebApi.Models;

public class CreateUserDto : IMapWith<CreateUserCommand>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ShortName { get; set; }
    public string Description { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateUserCommand, CreateUserDto>()
            .ForMember(dto => dto.FirstName, 
                opt => opt.MapFrom(src => src.Firstname))
            .ForMember(dto => dto.LastName,
                opt => opt.MapFrom(src => src.Lastname))
            .ForMember(dto => dto.ShortName,
                opt => opt.MapFrom(src => src.Shortname))
            .ForMember(dto => dto.Description,
                opt => opt.MapFrom(src => src.Description))
            .ForMember(dto => dto.PhoneNumber,
                opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dto => dto.Password,
                opt => opt.MapFrom(src => src.Password));
    }
}