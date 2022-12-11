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

    public List<PostDTO> GetPostByPrice(int priceMin, int priceMax)
    {
        if (priceMin >= priceMax)
            throw new ArgumentException("Wrong price range");
        return GetAllPosts().Where(p => p.Price >= priceMin && p.Price <= priceMax).ToList();
    }

    public List<PostDTO> GetPostByCategoryAndPrice(FilterSearchDTO catPriceDto)
    {
        if (catPriceDto == null || catPriceDto.DTO.min >= catPriceDto.DTO.max)
            throw new ArgumentException("Wrong category/price argument");
        return GetAllPosts()
            .Where(p=> catPriceDto.DTO.ids.Contains(p.Category) && (p.Price >= catPriceDto.DTO.min && p.Price <= catPriceDto.DTO.max))
            .ToList();
    }

    public List<PostDTO> GetPostByTitleAndDescription(string query)
    {
        if (query == "")
            throw new ArgumentException("bad query");
        //Todo: dd the test on the paraneter of string as well as min max etc.. 
        return GetAllPosts()
            .Where(p => p.Title.Contains(query) || p.Description.Contains(query))
            .ToList();
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