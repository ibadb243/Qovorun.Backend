using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseConstroller : Controller
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    
    internal Guid UserId => User.Identity!.IsAuthenticated ? Guid.Parse(User.Identity.Name) : Guid.Empty;
}