using Application.CQRS.User.Commands.CreateCommand;
using Application.CQRS.User.Queries.GetDetailsQuery;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
public class UserController : BaseConstroller
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> Get(Guid userId)
    {
        var query = new GetUserDetailsQuery
        {
            RequesterId = UserId,
            UserId = userId
        };
        
        var result = await Mediator.Send(query);
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
    {
        var command = _mapper.Map<CreateUserCommand>(dto);
        
        var result = await Mediator.Send(command);
        
        return Ok(result);
    }
}