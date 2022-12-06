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
        List<User> allUsers = _userRepository.GetAllUsers();
        Dictionary<int, User> hashUsers = new Dictionary<int, User>();
        foreach (User user in allUsers)
        {
            hashUsers.Add(user.Id,user);
        }
        foreach (Post post in posts)
        {
            PostDTO postDto = new PostDTO();
            postDto.Id = post.Id;
            postDto.UserName = hashUsers[post.UserId].userName;
            postDto.Price = post.Price;
            postDto.Authority = hashUsers[post.UserId].RoleId;
            postDto.Email = hashUsers[post.UserId].Email;
            postDto.Title = post.Title;
            postDto.Address = hashUsers[post.UserId].Address;
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
        Console.WriteLine("Post Length: "+posts.Count);
        List<PostDTO> postsFormated = new List<PostDTO>();
        foreach (Post post in posts)
        {
            
            PostDTO postDto = new PostDTO();
            postDto.Id = post.Id;
            postDto.UserName = user.userName;
            postDto.Price = post.Price;
            postDto.Authority = user.RoleId;
            postDto.Email = user.Email;
            postDto.Title = post.Title;
            postDto.Address = user.Address;
            postDto.Description = post.Description;
            postDto.Category = post.Category;
            postsFormated.Add(postDto);
        }
        return postsFormated;
    }

    public void CreatePost(CreatePostDTO dto)
    {
        Console.WriteLine("dto: "+dto.Description);
        User user = _userRepository.GetUserByEmail(dto.Email);
        
        Post post = new Post()
        {
            UserId = user.Id,
            User = user,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            Category = dto.Category,
            Date = DateTime.Now
        };
        _bikeShopRepository.CreatePost(post);
    }

    public Post GetPost(int id)
    {
        return _bikeShopRepository.GetPost(id);
    }

    public List<Post> GetPostByCategory(int[] listId)
    {
        return _bikeShopRepository.getPostByCategory(listId);
    }
}