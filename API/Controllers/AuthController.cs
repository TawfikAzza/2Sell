using API.DTOs;
using API.Services;
using API.Validators;
using AutoMapper;
using Core;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _auth;
    private readonly IMapper _mapper;
    public AuthController(IAuthenticationService auth, IMapper mapper)
    {
        _auth = auth;
        _mapper = mapper;
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