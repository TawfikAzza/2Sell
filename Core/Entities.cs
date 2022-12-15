namespace Core;

public class Post
{
    public int Id { get; set; }
    public int Category { get; set; }
    public double Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    //Connection to User Table
    public int UserId { get; set; }
    public User User { get; set; } 
    public string Img { get; set; }
    public virtual List<Comment>? Comments { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string userName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public virtual List<Post>? Posts { get; set; }
    // public int RoleId { get; set; }
    public string? Img { get; set; }
    public int RoleId { get; set; }
    
    //public byte[] Image { get; set; }
    //public List<Roles>? Roles { get; set; }
}

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    
    public int PostID { get; set; }
    public Post Post { get; set; }
}
