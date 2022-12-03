using API.DTOs;
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
    public List<PostDTO> GetAllPosts()
    {
        List<Post> posts =  _bikeShopRepository.GetAllPosts();
        List<PostDTO> postsFormated = new List<PostDTO>();
        foreach (Post post in posts)
        {
            PostDTO postDto = new PostDTO();
            postDto.UserName = post.User.userName;
            postDto.Price = post.Price;
            postDto.Authority = post.User.RoleId;
            postDto.Email = post.User.Email;
            postDto.Title = post.Title;
            postDto.Address = post.User.Address;
            postDto.Description = post.Description;
            postsFormated.Add(postDto);
        }

        return postsFormated;
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

    public List<PostDTO> GetAllPostFromUser(string username)
    {
        try
        {
            _userRepository.GetUserByUserName(username);
        }
        catch(KeyNotFoundException ex)
        {
            throw new Exception(ex.Message);
        }

        User user = _userRepository.GetUserByUserName(username);
        List<Post> posts = _bikeShopRepository.GetAllPostsFromUser(user);
        List<PostDTO> postsFormated = new List<PostDTO>();
        foreach (Post post in posts)
        {
            PostDTO postDto = new PostDTO();
            postDto.UserName = post.User.userName;
            postDto.Price = post.Price;
            postDto.Authority = post.User.RoleId;
            postDto.Email = post.User.Email;
            postDto.Title = post.Title;
            postDto.Address = post.User.Address;
            postDto.Description = post.Description;
            postDto.Category = post.Category;
            postsFormated.Add(postDto);
        }
        return postsFormated;
    }

    public void CreatePost(CreatePostDTO dto)
    {
        Post post = new Post()
        {
            
        };
        
    }
}