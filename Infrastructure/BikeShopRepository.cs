using System.Globalization;
using System.IO.Enumeration;
using API.DTOs;
using Application.Interfaces;
using Core;

namespace Infrastructure;

public class BikeShopRepository: IBikeShopRepository
{
    private readonly BikeShopDbContext _bikeShopDbContext;

    public BikeShopRepository(BikeShopDbContext context)
    {
        _bikeShopDbContext = context;
    }
    public List<Post> GetAllBikes()
    {
        throw new NotImplementedException();
    }

    public List<Post> GetAllPostsFromUser(User user)
    {
        User userSearched = _bikeShopDbContext.UsersTable.Find(user.Id);
        
        return _bikeShopDbContext.PostTable
            .Where(p=> p.UserId==userSearched.Id)
            .OrderByDescending(p=> p.Date)
            .ToList();
    }

    public List<Post> GetAllPosts()
    {
        return _bikeShopDbContext.PostTable.ToList();
    }

    public void CreatePost(Post post)
    {
        
        _bikeShopDbContext.PostTable.Add(post);
        _bikeShopDbContext.SaveChanges();
    }

    public Post GetPost(int id)
    {
       return _bikeShopDbContext.PostTable.Find(id);
    }

    public List<Post> getPostByCategory(int[] listId)
    {
        return _bikeShopDbContext.PostTable.Where(p=> listId.Contains(p.Category)).ToList();
    }

    public void DeletePost(int id)
    {
        Post post = _bikeShopDbContext.PostTable.Single(p => p.Id==id);
        _bikeShopDbContext.PostTable.Remove(post);
        _bikeShopDbContext.SaveChanges();
    }

    public void AddComment(CommentDTO dto)
    {
        Comment comment = new Comment();
        comment.Content = dto.Content;
        comment.PostID = dto.PostId;
        comment.Author = dto.Author;
        comment.Date = DateTime.Now;
        _bikeShopDbContext.CommentTable.Add(comment);
        _bikeShopDbContext.SaveChanges();
    }

    public List<CommentDTO> GetAllCommentFromPost(int postId)
    {
        Console.WriteLine();
        Post post = _bikeShopDbContext.PostTable.Single(p=> p.Id ==postId);
        List<User> users = _bikeShopDbContext.UsersTable.ToList();
        
        Dictionary<string, string> userImage = new Dictionary<string, string>();
        
            
        foreach (User user in users)
        {
            
            userImage.Add(user.userName,user.Img ?? "");
        }
        List<CommentDTO> commentDtos = new List<CommentDTO>();
        foreach (Comment comment in _bikeShopDbContext.CommentTable.Where(c=> c.PostID == postId).ToList())
        {
            CommentDTO commentDto = new CommentDTO();
            commentDto.Content = comment.Content;
            commentDto.PostId = comment.PostID;
            commentDto.Author = comment.Author;
            commentDto.Avatar = userImage[comment.Author] ?? "";
            commentDto.Date=comment.Date.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Avatar : "+commentDto.Avatar);
            commentDtos.Add(commentDto);
        }

        return commentDtos;
    }
    


    public void CreateDB()
    {
        //Console.WriteLine("Method Called");
        _bikeShopDbContext.Database.EnsureDeleted();
        _bikeShopDbContext.Database.EnsureCreated();
    }
}