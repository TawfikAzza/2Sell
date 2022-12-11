using System.Text.Json.Serialization;
using API.DTOs;
using Application.Interfaces;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
<<<<<<< HEAD
=======
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
>>>>>>> Develop

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
<<<<<<< HEAD
=======

    [AllowAnonymous]
    [HttpGet]
    [Route("ViewPost/{id}")]
    public ActionResult<PostDTO> GetPost([FromRoute] int id)
    {
        return Ok(_bikeShopService.GetPost(id));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("SearchCategories/{dto}")]
    public ActionResult<List<PostDTO>> GetPostByCategory([FromRoute] string dto)
    {
        
        FilterSearchDTO filterSearchDto = JsonConvert.DeserializeObject<FilterSearchDTO>(dto);
        
        switch (filterSearchDto.OperationType)
        {
            case 1:
                //CategoryDTO
                Console.WriteLine(filterSearchDto.DTO.ids[0]);
                return Ok(_bikeShopService.GetPostByCategory(filterSearchDto.DTO.ids));
                break;
            case 2:
                //PriceDTO
                //PriceDTO PriceDTO = JsonConvert.DeserializeObject<PriceDTO>(dto);
               // Console.WriteLine("price: "+PriceDTO.min+" max: "+PriceDTO.max);
                Console.WriteLine(filterSearchDto.DTO.max);
                return Ok(_bikeShopService.GetPostByPrice(filterSearchDto.DTO.min, filterSearchDto.DTO.max));
                break;
            case 3:
                //CatPriceDTO
                return Ok(_bikeShopService.GetPostByCategoryAndPrice(filterSearchDto));
            case 4:
                return Ok(_bikeShopService.GetPostByTitleAndDescription(filterSearchDto.DTO.args));
                break;
            default:
                break;
                
        }
        throw new ArgumentException("Wrong search filter data");

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
>>>>>>> Develop
}