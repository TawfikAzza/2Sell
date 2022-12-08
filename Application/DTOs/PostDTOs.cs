namespace API.DTOs;

public class PostDTO
{
    public int? Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public double Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Authority { get; set; }
    public string Address { get; set; }
    public int Category { get; set; }
}

public class CreatePostDTO
{
    public string Email { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Category { get; set; }
}