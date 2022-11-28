using API.DTOs;
using API.Services;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _auth;
    public AuthController(IAuthenticationService auth)
    {
        _auth = auth;
    }
    [HttpPost]
    [Route("login")]
    public IActionResult Login(LoginDTO dto)
    {
        try
        {
            return Ok(_auth.Login(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    [Route("register")]
    public ActionResult<string> Register(RegisterDTO dto)
    {
        try
        {
            return Ok(_auth.Register(dto));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}