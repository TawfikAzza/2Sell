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
        User userSearched = _bikeShopDbContext.UsersTable.Find(user.Id);
        
        return _bikeShopDbContext.PostTable
            .Where(p=> p.UserId==userSearched.Id)
            .OrderByDescending(p=> p.Date)
            .ToList();
    }

    public List<Post> GetAllPosts()
    {
        return _bikeShopDbContext.PostTable.ToList();
    }

    public void CreatePost(Post post)
    {
        Console.WriteLine("Post: "+post.User.Id);
        _bikeShopDbContext.PostTable.Add(post);
        _bikeShopDbContext.SaveChanges();
    }


    public void CreateDB()
    {
        //Console.WriteLine("Method Called");
        _bikeShopDbContext.Database.EnsureDeleted();
        _bikeShopDbContext.Database.EnsureCreated();
    }
}