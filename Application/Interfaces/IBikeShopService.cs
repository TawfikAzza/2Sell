using Core;

namespace Application.Interfaces;

public interface IBikeShopService
{
    public List<Post> GetAllPosts();
    public void CreateDB();
    public void GetAllBikes();
    public void GetUserByEmail(string email);
}