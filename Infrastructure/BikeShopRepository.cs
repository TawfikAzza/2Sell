using Application.Interfaces;
using Core;

namespace Infrastructure;

public class BikeShopRepository: IBikeShopRepository
{
    private readonly BikeShopDbContext _bikeShopDbContext;

    public BikeShopRepository(BikeShopDbContext context)
    {
        _bikeShopDbContext = context;
    }
    public List<Post> GetAllBikes()
    {
        throw new NotImplementedException();
    }

    public List<Post> GetAllPostsFromUser(User user)
    {
        return _bikeShopDbContext.PostTable
            .Where(p=> p.User.userName == user.userName)
            .OrderByDescending(p=> p.Date)
            .ToList();
    }

    public List<Post> GetAllPosts()
    {
        return _bikeShopDbContext.PostTable.ToList();
    }


    public void CreateDB()
    {
        //Console.WriteLine("Method Called");
        _bikeShopDbContext.Database.EnsureDeleted();
        _bikeShopDbContext.Database.EnsureCreated();
    }
}