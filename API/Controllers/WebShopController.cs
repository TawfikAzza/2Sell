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

    [AllowAnonymous]
    [HttpPost]
    [Route("UpdateProfile")]
    public ActionResult<RegisterDTO> UpdateProfile(RegisterDTO dto)
    {
        Console.WriteLine("Update Profile:"+dto.FirstName);
        return _userService.UpdateUser(dto);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetAllPostsFromUser/{username}")]
    public ActionResult<List<PostDTO>> GetAllPostFromUser([FromRoute] string username)
    {
        return _bikeShopService.GetAllPostFromUser(username);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetAllPosts")]
    public ActionResult<List<PostDTO>> GetAllPosts()
    {
        return _bikeShopService.GetAllPosts();
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("CreatePost")]
    public void CreatePost(CreatePostDTO dto)
    {
        _bikeShopService.CreatePost(dto);
    }
}