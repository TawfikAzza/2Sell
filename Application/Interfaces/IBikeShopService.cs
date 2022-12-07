using API.DTOs;
using Core;

namespace Application.Interfaces;

public interface IBikeShopService
{
    public List<PostDTO> GetAllPosts();
    public void CreateDB();

    List<PostDTO> GetAllPostFromUser(string username);
    void CreatePost(CreatePostDTO dto);

    Post GetPost(int id);
    List<Post> GetPostByCategory(int[] listId);
    public List<User> GetAllUsers();
}