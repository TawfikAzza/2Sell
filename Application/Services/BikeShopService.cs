using Application.Interfaces;
using Core;

namespace Application.Services;

public class BikeShopService : IBikeShopService
{
    private readonly IBikeShopRepository _bikeShopRepository;
    private readonly IUserRepository _userRepository;
    public BikeShopService(IBikeShopRepository bikeRepository,IUserRepository userRepository)
    {
        _bikeShopRepository = bikeRepository;
        _userRepository = userRepository;
    }
    public List<Post> GetAllPosts()
    {
        throw new NotImplementedException();
    }

    public void CreateDB()
    {
        _bikeShopRepository.CreateDB();
    }

    public void GetAllBikes()
    {
        _bikeShopRepository.GetAllBikes();
    }

    public void GetUserByEmail(string email)
    {
        _userRepository.GetUserByEmail(email);
    }
}