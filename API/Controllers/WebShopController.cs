using API.DTOs;
using Application.Interfaces;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WebShopController : ControllerBase
{
    private readonly IBikeShopService _bikeShopService;
    private readonly IUserService _userService;
    public WebShopController(IBikeShopService bikeService,IUserService userService)
    {
        _bikeShopService = bikeService;
        _userService = userService;
    }
    [Authorize("AdminPolicy")]
    [HttpGet]
    [Route("CreateDB")]
    public void CreateDB()
    {
        _bikeShopService.CreateDB();
    }
    [Authorize("UserPolicy")]
    [HttpGet]
    [Route("GetAllUsers")]
    public void GetAllBikes()
    {
        _bikeShopService.GetAllBikes();
    }

    //[Authorize("UserPolicy")]
    [AllowAnonymous]
    [HttpGet]
    [Route("GetUserByEmail/{email}")]
    public ActionResult<UserDTO> GetUserByEmail([FromRoute] string email)
    {
        Console.WriteLine("User email :"+email);
        return _userService.GetUserByEmail(email);
    }
    
}