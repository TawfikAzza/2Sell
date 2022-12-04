using Core;

namespace Application.Interfaces;

public interface IBikeShopRepository
{
    public void CreateDB();
    public List<Post> GetAllBikes();

    List<Post> GetAllPostsFromUser(User user);
    List<Post> GetAllPosts();
}