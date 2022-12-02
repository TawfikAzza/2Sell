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

    

    public void CreateDB()
    {
        Console.WriteLine("Method Called");
        //_bikeShopDbContext.Database.EnsureDeleted();
        //_bikeShopDbContext.Database.EnsureCreated();
    }
}