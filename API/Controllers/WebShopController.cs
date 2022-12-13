using System.Text.Json.Serialization;
using API.DTOs;
using Application.Interfaces;
using AutoMapper;
using Core;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

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
    public void CreatePost(CreatePostDTO createPostDto)
    {

        /*StringValues titleValue="";
        StringValues priceValue = "";
        StringValues categoryValue = "";
        StringValues descritpionValue = "";
        StringValues imgValue = "";
        Request.Form.TryGetValue("title", out titleValue);
        Request.Form.TryGetValue("price", out priceValue);
        Request.Form.TryGetValue("category", out categoryValue);
        Request.Form.TryGetValue("description", out descritpionValue);
        Request.Form.TryGetValue("img", out imgValue);
        int price = Convert.ToInt32(priceValue);
        int category = Convert.ToInt32(categoryValue);
        Console.WriteLine("Title :"+titleValue+" price: "+price+" category : "+category+" description : "+descritpionValue
        +" img : "+imgValue);*/
        //Console.WriteLine("DTO"+dto.Img);
        _bikeShopService.CreatePost(createPostDto);
        //Console.WriteLine("DTO :"+createPostDto.Email);
    }

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
}