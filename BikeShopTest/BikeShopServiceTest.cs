using API.DTOs;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Core;
using Moq;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace BikeShopTest;

public class BikeShopServiceTest 
{
    User user1 = new User()
    {
        Id = 1,
        Address = "Address 1",
        Email = "user1@email.com",
        FirstName = "User 1",
        LastName = "Last Name",
        PhoneNumber = "456798",
        PostalCode = "4657",
        RoleId = 1,
        userName = "user1",
        PasswordHash = "jfdh",
        Salt = "65446666666"
    };
    User user2 = new User()
    {
        Id = 2,
        Address = "Address 2",
        Email = "user2@email.com",
        FirstName = "User 2",
        LastName = "Last Name",
        PhoneNumber = "456798",
        PostalCode = "4657",
        RoleId = 1,
        userName = "user2",
        PasswordHash = "jfdh",
        Salt = "65446666666"
    };
    User user3 = new User()
    {
        Id = 3,
        Address = "Address 3",
        Email = "user3@email.com",
        FirstName = "User 3",
        LastName = "Last Name",
        PhoneNumber = "456798",
        PostalCode = "4657",
        RoleId = 1,
        userName = "user3",
        PasswordHash = "jfdh",
        Salt = "65446666666"
    };
    User user4 = new User()
    {
        Id = 4,
        Address = "Address 4",
        Email = "user4@email.com",
        FirstName = "User 4",
        LastName = "Last Name",
        PhoneNumber = "456798",
        PostalCode = "4657",
        RoleId = 1,
        userName = "user4",
        PasswordHash = "jfdh",
        Salt = "65446666666"
    };
    Post post1 = new Post()
    {
        Id = 1,
        Category = 1,
        Description = "Description 1",
        Price = 1236,
        Title = "Post1",
        Date = new DateTime(2022,12,1),
        UserId = 1
    }; 
    Post post2 = new Post()
    {
        Id = 2,
        Category = 2,
        Description = "Description 2",
        Price = 1586,
        Title = "Post2",
        Date = new DateTime(2022,12,2),
        UserId = 2
    };
    Post post3 = new Post()
    {
        Id = 3,
        Category = 3,
        Description = "Description 3",
        Price = 2578,
        Title = "Post3",
        Date = new DateTime(2022,12,3),
        UserId = 3
    };
    Post post4 = new Post()
    {
        Id = 4,
        Category = 3,
        Description = "Description 4",
        Price = 455445,
        Title = "Post4",
        Date = new DateTime(2022,12,4),
        UserId = 1
    };
    UserDTO userDto1 = new UserDTO()
    {
        Address = "Address1",
        Email = "user1@email.com",
        FirstName = "User 1",
        LastName = "Last Name",
        userName = "user1",
        PhoneNumber = "456789",
        PostalCode = "6874",
        RoleId = 1
    };
    UserDTO userDto2 = new UserDTO()
    {
        Address = "Address2",
        Email = "user2@email.com",
        FirstName = "User 2",
        LastName = "Last Name",
        userName = "user2",
        PhoneNumber = "456789",
        PostalCode = "6874",
        RoleId = 1
    };
    UserDTO userDto3 = new UserDTO()
    {
        Address = "Address3",
        Email = "user3@email.com",
        FirstName = "User 3",
        LastName = "Last Name",
        userName = "user3",
        PhoneNumber = "456789",
        PostalCode = "6874",
        RoleId = 1
    };
    PostDTO postDTO1 = new PostDTO()
    {
        Id = 1,
        Category = 1,
        Description = "Description 1",
        Price = 1236,
        Title = "Post1",
        Address = "Address 1",
        Authority = 1,
        Email = "user1@email.com",
        UserName = "user1"
    };
    PostDTO postDTO2 = new PostDTO()
    {
        Id = 2,
        Category = 2,
        Description = "Description 2",
        Price = 1586,
        Title = "Post2",
        Address = "Address 2",
        Authority = 1,
        Email = "user2@email.com",
        UserName = "user2"
    };
    PostDTO postDTO3 = new PostDTO()
    {
        Id = 3,
        Category = 3,
        Description = "Description 3",
        Price = 2578,
        Title = "Post3",
        Address = "Address 3",
        Authority = 1,
        Email = "user3@email.com",
        UserName = "user3"
    };
    PostDTO postDTO4 = new PostDTO()
    {
        Id = 4,
        Category = 3,
        Description = "Description 4",
        Price = 455445,
        Title = "Post4",
        Address = "Address 1",
        Authority = 1,
        Email = "user1@email.com",
        UserName = "user1"
    };
    List<Post> listPost = new List<Post>(); 
    List<PostDTO> listPostDTO = new List<PostDTO>();
    List<User> listUser = new List<User>();
    List<UserDTO> listUserDTO = new List<UserDTO>();
    List<Post> emptyPostList = new List<Post>();
    Mock<IBikeShopRepository> bikeRepository = new Mock<IBikeShopRepository>();
    Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
    
    private readonly ITestOutputHelper output;
    private readonly IMapper _mapper;
    public BikeShopServiceTest(ITestOutputHelper output)
    {
        listPost.Add(post1);
        listPost.Add(post2);
        listPost.Add(post3);
        listPost.Add(post4);
        
        listUser.Add(user1);
        listUser.Add(user2);
        listUser.Add(user3);
        
        listPostDTO.Add(postDTO1);
        listPostDTO.Add(postDTO2);
        listPostDTO.Add(postDTO3);
        listPostDTO.Add(postDTO4);
        this.output = output;
       // _mapper = new Mock<IMapper>();
   
    }
    [Fact]
    public void BikeShopServiceGetAllPosts()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        List<PostDTO> expected = service.GetAllPosts();
        Assert.Equal(JsonConvert.SerializeObject(expected),JsonConvert.SerializeObject(listPostDTO));
        bikeRepository.Verify(b=> b.GetAllPosts(),Times.Once);
    }
    [Fact]
    public void BikeShopServiceGetAllPostsWithNoPostsExpectInvalidProgramException()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(emptyPostList);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        InvalidProgramException ex = Assert.Throws<InvalidProgramException>(() => service.GetAllPosts());
        Assert.Equal("No posts in the database", ex.Message);
        bikeRepository.Verify(b=> b.GetAllPosts(),Times.Once);
    }
    [Fact]
    public void BikeShopServiceGetPost()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        PostDTO postDtoExpected = service.GetPost(1);
        Assert.True(JsonConvert.SerializeObject(postDtoExpected).Equals(JsonConvert.SerializeObject(postDTO1)));
        bikeRepository.Verify(b=> b.GetAllPosts(),Times.Once);
    }

    [Fact]
    public void BikeShopServiceGetPostWithIDPostDoesNotExistExpectArgumentException()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        ArgumentException ex = Assert.Throws<ArgumentException>(() => service.GetPost(6));
        Assert.Equal("No Post with this id", ex.Message);
        bikeRepository.Verify(b=> b.GetPost(6),Times.Never);
    }
    
    [Fact]
    public void BikeShopServiceGetPostByCategory()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);

        int[] listCategory = new[] { 1, 2 };
        List<PostDTO> listExpected = new List<PostDTO>() { postDTO2, postDTO1 };
        List<PostDTO> ActualList = service.GetPostByCategory(listCategory);
        Assert.True(JsonConvert.SerializeObject(listExpected).Equals(JsonConvert.SerializeObject(ActualList)));
    }
    [Fact]
    public void BikeShopServiceGetPostByCategoryWithWrongCategoryIDExceptArgumentException()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);

        int[] listCategory = new[] { 9,8 };
        
        ArgumentException ex = Assert.Throws<ArgumentException>(() => service.GetPostByCategory(listCategory));
        Assert.Equal("No post with such category", ex.Message);
        bikeRepository.Verify(b=> b.getPostByCategory(listCategory),Times.Never);
    }
    [Fact]
    public void BikeShopServiceGetAllUsers()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        List<User> listExpected = new List<User>() { user1, user2, user3 };
        List<User> ActualList = service.GetAllUsers();
        Assert.True(JsonConvert.SerializeObject(listExpected).Equals(JsonConvert.SerializeObject(ActualList)));
    }

    [Fact]
    public void GetAllPostFromUser()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        List<PostDTO> ExpectedList = new List<PostDTO>() { postDTO1,postDTO4 };
        List<PostDTO> ActualList = service.GetAllPostFromUser(user1.userName);
        output.WriteLine(JsonConvert.SerializeObject(ExpectedList));
        output.WriteLine(JsonConvert.SerializeObject(ActualList));
        Assert.True(JsonConvert.SerializeObject(ExpectedList).Equals(JsonConvert.SerializeObject(ActualList)));
    }
    [Fact]
    public void GetAllPostFromUserWithInvalidUserNameExpectArgumentException()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        ArgumentException ex = Assert.Throws<ArgumentException>(() => service.GetAllPostFromUser("FalseUser"));
        Assert.Equal("No such user in database", ex.Message);
    }
    [Fact]
    public void GetAllPostFromUserWithNoPostReturnedExpectArgumentException()
    {
        IBikeShopRepository mockBikeRepository = bikeRepository.Object;
        IUserRepository mockUserRepository = userRepository.Object;
        bikeRepository.Setup(b => b.GetAllPosts()).Returns(listPost);
        userRepository.Setup(u => u.GetAllUsers()).Returns(listUser);
        IBikeShopService service = new BikeShopService(mockBikeRepository, mockUserRepository);
        ArgumentException ex = Assert.Throws<ArgumentException>(() => service.GetAllPostFromUser(user4.userName));
        Assert.Equal("No such user in database", ex.Message);
    }
}