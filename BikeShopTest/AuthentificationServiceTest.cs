using System.Diagnostics;
using API.DTOs;
using API.Services;
using Application.Helpers;
using Application.Interfaces;
using Application.Services;
using Core;
using Infrastructure;
using Microsoft.Extensions.Options;
using Moq;

namespace BikeShopTest;

public class AuthentificationServiceTest
{
    [Fact]
    public void LoginTestWithValidArguments()
    {
        Mock<IUserRepository> mockRepository = new Mock<IUserRepository>();
        Mock<IBikeShopRepository> bikeRepository = new Mock<IBikeShopRepository>();
        IUserRepository repository = mockRepository.Object;
        IBikeShopRepository bikeShopRepository = bikeRepository.Object;
        LoginDTO dto = new LoginDTO()
        {
            Email = "user",
            Password = "test"
        };
        User user = new User()
        {
            Address = "Test",
            Email = "user",
            PasswordHash = "$2a$11$R0jyH.4BETcon24oem/EHujWck04p9q63aTeJKk12sOuwP3vfk6eC",
            Salt = "902359781451845317017036110225106103221351482222141372142431232211341047196939",
        };
        mockRepository.Setup(u => u.GetUserByEmail("user")).Returns(user);
        IOptions<AppSettings> appSettings;
    }
}