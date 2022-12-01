using Application.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WebShopController : ControllerBase
{
    private readonly IBikeShopRepository _bikeShopRepository;
    public WebShopController(IBikeShopRepository repository)
    {
        _bikeShopRepository = repository;
    }
    [AllowAnonymous]
    [HttpGet]
    [Route("CreateDB")]
    public void CreateDB()
    {
        _bikeShopRepository.CreateDB();
    }
    [Authorize("UserPolicy")]
    [HttpGet]
    [Route("GetAllUsers")]
    public void GetAllBikes()
    {
        _bikeShopRepository.GetAllBikes();
    }
}