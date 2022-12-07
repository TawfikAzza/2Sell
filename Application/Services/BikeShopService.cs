using System.ComponentModel;
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

    public List<User> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }
    public List<PostDTO> GetAllPosts()
    {
        List<Post> posts = GetAllPostDB().ToList();
        if (posts.Count < 1)
            throw new Exception("No posts in the database");
        List<PostDTO> postsFormated = new List<PostDTO>();
        List<User> allUsers = GetAllUserDB();
        if (allUsers.Count < 1)
            throw new Exception("No users in the database");
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
        User userSearched = new User();
        try
        {
            userSearched = GetAllUserDB().Where(u=> u.userName.Equals(username)).Single();
        }
        catch(KeyNotFoundException ex)
        {
            throw new Exception(ex.Message);
        }
        
        User user = userSearched;
        List<Post> posts = GetAllPostDB().Where(p=> p.UserId==user.Id).ToList();
        if (posts.Count < 1)
            throw new Exception("No posts associated to this user" + user.userName);
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
        User user = new User();
        try
        {
            user = GetAllUserDB().Where(u => u.Email.Equals(dto.Email)).Single();
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception("No such User in the database");
        }
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
        Post postSearched = new Post();
        try
        {
            postSearched = GetAllPostDB().Where(p => p.Id == id).Single();
        }
        catch (InvalidOperationException ex)
        {
            throw new Exception("Not Post with this id");
        }
        return postSearched;
    }

 
    public List<Post> GetPostByCategory(int[] listId)
    {
        List<Post> listPost = new List<Post>();
        listPost =  GetAllPostDB()
            .Where(p=> listId.Contains(p.Category))
            .OrderByDescending(p=> p.Date)
            .ToList();
        if (listPost.Count == 0)
        {
            throw new InvalidEnumArgumentException("No post with such category");
        }
        return listPost;
    }

    public List<User> GetAllUserDB()
    {
        return _userRepository.GetAllUsers();
    }

    public List<Post> GetAllPostDB()
    {
        return _bikeShopRepository.GetAllPosts();
    }
    
}