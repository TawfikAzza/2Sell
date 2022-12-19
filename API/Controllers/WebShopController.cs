using System.Text.Json.Serialization;
using API.DTOs;
using API.Validators;
using Application.Interfaces;
using AutoMapper;
using Core;
using FluentValidation;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using UserValidator = Application.Validators.UserValidator;

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
    [Authorize("AdminPolicy")]
    [HttpGet]
    [Route("CreateDB")]
    public void CreateDB()
    {
        _bikeShopService.CreateDB();
    }
    //[AllowAnonymous]
    [Authorize("AdminPolicy")]
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
        if (email == null)
        {
            BadRequest("Invalid email");
        }
        return _userService.GetUserByEmail(email);
    }

    [Authorize("UserPolicy")]
    [HttpPost]
    [Route("UpdateProfile")]
    public ActionResult<RegisterDTO> UpdateProfile(RegisterDTO dto)
    {
        UserValidator validator = new UserValidator();
        var validate = validator.Validate(dto);
        if (!validate.IsValid)
        {
            BadRequest("Wrong user data sent");
        }
        return Ok(_userService.UpdateUser(dto));
    }
    
    [Authorize(Roles = "1,0")]
    [HttpGet]
    [Route("GetAllPostsFromUser/{username}")]
    public ActionResult<List<PostDTO>> GetAllPostFromUser([FromRoute] string username)
    {
        Console.WriteLine("User name "+username);
        return Ok(_bikeShopService.GetAllPostFromUser(username));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetAllPosts")]
    public ActionResult<List<PostDTO>> GetAllPosts()
    {
        return Ok(_bikeShopService.GetAllPosts());
    }
    
    [Authorize("UserPolicy")]
    [HttpPost]
    [Route("CreatePost")]
    public void CreatePost(CreatePostDTO createPostDto)
    {
        CreatePostValidator validator = new CreatePostValidator();
        var validate = validator.Validate(createPostDto);
        if (!validate.IsValid)
        {
            BadRequest("Worng data sent");
        }
        _bikeShopService.CreatePost(createPostDto);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("ViewPost/{id}")]
    public ActionResult<PostDTO> GetPost([FromRoute] int id)
    {
        if (id <= 0)
            BadRequest("Invalid id sent");
        return Ok(_bikeShopService.GetPost(id));
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("SearchCategories/{dto}")]
    public ActionResult<List<PostDTO>> GetPostByCategory([FromRoute] string dto)
    {
        FilterSearchDTO filterSearchDto = JsonConvert.DeserializeObject<FilterSearchDTO>(dto);
        string message = "";
        switch (filterSearchDto.OperationType)
        {
            case 1:
                //CategoryDTO
                Console.WriteLine(filterSearchDto.DTO.ids[0]);
                try
                {
                    return Ok(_bikeShopService.GetPostByCategory(filterSearchDto.DTO.ids));
                }
                catch (ArgumentException ex)
                {
                    message = ex.Message;
                }
                
                break;
            case 2:
                //PriceDTO
                //PriceDTO PriceDTO = JsonConvert.DeserializeObject<PriceDTO>(dto);
               // Console.WriteLine("price: "+PriceDTO.min+" max: "+PriceDTO.max);
                Console.WriteLine(filterSearchDto.DTO.max);
                try
                {
                    return Ok(_bikeShopService.GetPostByPrice(filterSearchDto.DTO.min, filterSearchDto.DTO.max));
                } catch (ArgumentException ex)
                {
                    message = ex.Message;
                }
                
                break;
            case 3:
                //CatPriceDTO
                try
                {
                    return Ok(_bikeShopService.GetPostByCategoryAndPrice(filterSearchDto));
                } catch (ArgumentException ex)
                {
                    message = ex.Message;
                }

                break;
            case 4:
                try
                {
                    return Ok(_bikeShopService.GetPostByTitleAndDescription(filterSearchDto.DTO.args));
                } catch (ArgumentException ex)
                {
                    message = ex.Message;
                }
                break;
            default:
                break;
                
        }

        BadRequest(message);
        throw new Exception(message);

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

    [Authorize("AdminPolicy")]
    [HttpPost]
    [Route("ChangeBanStatus")]
    public void BanUser([FromBody] string email)
    {
        if (email == null)
            BadRequest("Invalid Email sent");
        if (email != "")
        {
            _userService.changeBanStatus(email);
        }
        else
        {
            BadRequest("Email incorrect");
        }
    }

    [Authorize(Roles= ("0,1"))]
    [HttpGet]
    [Route("DeletePost/{id}")]
    public void DeletePost([FromRoute] int id)
    {
        if (id <= 0)
            BadRequest("Invalid Post Id sent");
        _bikeShopService.DeletePost(id);
    }

    [Authorize(Roles = ("0,1"))]
    [HttpPost]
    [Route("AddComment")]
    public void AddComment([FromBody] CommentDTO dto)
    {
        Console.WriteLine("Comment : "+dto.Content);
        _bikeShopService.AddComment(dto);
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("GetAllCommentFromPost/{id}")]
    public ActionResult<List<CommentDTO>> GetAllCommentFromPost([FromRoute] int id)
    {
        Console.WriteLine("Id: "+id);
        return _bikeShopService.GetAllCommentFromPost(id);
    }

    [Authorize(Roles = ("0,1"))]
    [HttpPost]
    [Route("SendMail")]
    public void SendMail([FromBody] MailDTO mail)
    {
        _bikeShopService.SendMail(mail);
    }
}