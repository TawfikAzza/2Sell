using API.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;

namespace API.Controllers;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WebShopController : ControllerBase
{
    private readonly IBikeShopService _bikeShopService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public WebShopController(IBikeShopService bikeService,IUserService userService, IMapper mapper)
    {
        _bikeShopService = bikeService;
        _userService = userService;
        _mapper = mapper;
    }
    [AllowAnonymous]
    [HttpGet]
    [Route("CreateDB")]
    public void CreateDB()
    {
        _bikeShopService.CreateDB();
    }
    [AllowAnonymous]
    //[Authorize("UserPolicy")]
    [HttpGet]
    [Route("GetAllUsers")]
    public ActionResult<List<UserDTO>> GetAllUsers()
    {
        List<User> listUser = _bikeShopService.GetAllUsers();
        List<UserDTO> userDtos = new List<UserDTO>();
        if (listUser.Count > 0)
        {
            foreach (User user in listUser)
            {
                userDtos.Add(_mapper.Map<User,UserDTO>(user));
            } 
            return Ok(userDtos);
        }
        return BadRequest("No Users in Database");
       // return null;
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
        return Ok(_userService.UpdateUser(dto));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetAllPostsFromUser/{username}")]
    public ActionResult<List<PostDTO>> GetAllPostFromUser([FromRoute] string username)
    {
        return Ok(_bikeShopService.GetAllPostFromUser(username));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetAllPosts")]
    public ActionResult<List<PostDTO>> GetAllPosts()
    {
        return Ok(_bikeShopService.GetAllPosts());
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("CreatePost")]
    public void CreatePost(CreatePostDTO dto)
    {
        _bikeShopService.CreatePost(dto);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("ViewPost/{id}")]
    public ActionResult<PostDTO> GetPost([FromRoute] int id)
    {
        return Ok(_bikeShopService.GetPost(id));
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("SearchCategories")]
    public ActionResult<List<Post>> GetPostByCategory([FromBody] int[] listId)
    {
        Console.WriteLine("ListID :"+listId.Length);
        return Ok(_bikeShopService.GetPostByCategory(listId));
    }
    [AllowAnonymous]
    [HttpPost]
    [Route("UploadFileProfile")]
    public async Task<IActionResult> UploadFileProfile()
    {
        
        IFormFile file = Request.Form.Files.FirstOrDefault();
        StringValues email;
        Request.Form.TryGetValue("userEmail",out email);
        Console.WriteLine("Email: "+email);
        var originalFileName = Path.GetFileName(file.FileName);
        Console.WriteLine("Content type :"+Path.GetExtension(file.FileName));
        var extension = Path.GetExtension(file.FileName);
        var uniqueFileName = Path.GetRandomFileName()+extension;
        var uniqueFilePath = Path.Combine(@"..\frontend\src\assets\images\", uniqueFileName);
        Console.WriteLine("File: "+uniqueFilePath);
        using (var stream = System.IO.File.Create(uniqueFilePath))
        {
            await file.CopyToAsync(stream);
        }
        return Ok(true);
    }
}