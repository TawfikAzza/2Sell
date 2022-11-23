using Application.Interfaces;
using Infrastructure;

namespace API.Controllers;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class WebShopController : ControllerBase
{
    private readonly IBikeShopRepository _bikeShopRepository;
    public WebShopController(IBikeShopRepository repository)
    {
        _bikeShopRepository = repository;
    }

    [HttpGet]
    [Route("CreateDB")]
    public void CreateDB()
    {
        _bikeShopRepository.CreateDB();
    }
    
}