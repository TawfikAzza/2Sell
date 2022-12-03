using API.DTOs;
using Core;

namespace Application.Interfaces;

public interface IBikeShopService
{
    public List<PostDTO> GetAllPosts();
    public void CreateDB();
    public void GetAllBikes();
    public void GetUserByEmail(string email);
    List<PostDTO> GetAllPostFromUser(string username);
    void CreatePost(CreatePostDTO dto);
}