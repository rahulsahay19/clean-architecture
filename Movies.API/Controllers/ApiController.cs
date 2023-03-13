using Microsoft.AspNetCore.Mvc;

namespace Movies.API.Controllers;

[ApiVersion("1")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ApiController: ControllerBase
{
    
}