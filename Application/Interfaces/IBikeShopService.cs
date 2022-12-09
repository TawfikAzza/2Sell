using API.DTOs;
using Core;

namespace Application.Interfaces;

public interface IBikeShopService
{
    public List<PostDTO> GetAllPosts();
    public void CreateDB();

    List<PostDTO> GetAllPostFromUser(string username);
    void CreatePost(CreatePostDTO dto);

    PostDTO GetPost(int id);
  
    List<PostDTO> GetPostByCategory(int[] listId);
    public List<User> GetAllUsers();
    List<PostDTO> GetPostByPrice(int priceDtoMin, int priceDtoMax);
    List<PostDTO> GetPostByCategoryAndPrice(FilterSearchDTO filterSearchDto);
    List<PostDTO> GetPostByTitleAndDescription(string searchDtoArgs);
}