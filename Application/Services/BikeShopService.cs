using System.ComponentModel;
using API.DTOs;
using Application.Interfaces;
using AutoMapper;
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
        return _userRepository.GetAllUsers() ?? throw new InvalidProgramException("No user in database");
    }
    public List<PostDTO> GetAllPosts()
    {
        List<Post> posts = GetAllPostDB().ToList();
        if (posts.Count < 1)
            throw new InvalidProgramException("No posts in the database");
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
            postDto.Category = post.Category;
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

    public List<PostDTO> GetAllPostFromUser(string username)
    {
        User userSearched = new User();
        try
        {
            userSearched = GetAllUserDB().Where(u=> u.userName.Equals(username)).Single();
        }
        catch(InvalidOperationException ex)
        {
            throw new ArgumentException("No such user in database");
        }
        
        User user = userSearched;
        List<Post> posts = GetAllPostDB().Where(p=> p.UserId==user.Id).ToList();
        if (posts.Count < 1)
            throw new ArgumentException("No posts associated to this user" + user.userName);
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

    public PostDTO GetPost(int id)
    {
        Post postSearched = new Post();
        try
        {
            postSearched = GetAllPostDB().Where(p => p.Id == id).Single();
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentException("No Post with this id");
        }
        User user = null;
        try
        {
            user = GetAllUsers().Single(u => u.Id == postSearched.UserId);
        }
        catch (InvalidOperationException ex)
        {
            throw new ArgumentException("Not Post with this user id");
        }

        PostDTO postDto = new PostDTO()
        {
            Id = postSearched.Id,
            Address = user.Address,
            Authority = user.RoleId,
            Category = postSearched.Category,
            Description = postSearched.Description,
            Email = user.Email,
            Price = postSearched.Price,
            Title = postSearched.Title,
            UserName = user.userName
        };
        return postDto;
    }

    public List<PostDTO> GetPostByCategory(int[] listId)
    {
        List<Post> listPost = new List<Post>();
        Console.WriteLine("List count:"+listId.Length);
        for (int i = 0; i < listId.Length; i++)
        {
            Console.WriteLine("list "+listId[i]);
        }
        listPost =  GetAllPostDB()
            .Where(p=> listId.Contains(p.Category))
            .OrderByDescending(p=> p.Date)
            .ToList();
        if (listPost.Count == 0)
        {
            throw new ArgumentException("No post with such category");
        }

        List<User> allUsers = GetAllUserDB();
        if (allUsers.Count < 1)
            throw new Exception("No users in the database");
        Dictionary<int, User> hashUsers = new Dictionary<int, User>();
        foreach (User user in allUsers)
        {
            hashUsers.Add(user.Id,user);
        }
        List<PostDTO> listPostDTO = new List<PostDTO>();
        foreach (Post post in listPost)
        {
            PostDTO postDto = new PostDTO()
            {
                Id = post.Id,
                Address = hashUsers[post.UserId].Address,
                Authority = hashUsers[post.UserId].RoleId,
                Category = post.Category,
                Description = post.Description,
                Email = hashUsers[post.UserId].Email,
                Price = post.Price,
                Title = post.Title,
                UserName = hashUsers[post.UserId].userName
            };
            listPostDTO.Add(postDto);
        }
        return listPostDTO;
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