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
    public string Img { get; set; }
}
public class FilterSearchDTO
{
    public int  OperationType { get; set; }

    public Dto DTO { get; set; }
    /* public CategoryDTO? CategoryDto { get; set; }
     public PriceDTO? PriceDto { get; set; }
     public CatPriceDTO CatPriceDto { get; set; }
     public SearchDTO SearchDto{ get; set; }*/
}

public class Dto
{
    public string args { get; set; }
    public int[] ids { get; set; }
    public int min { get; set; }
    public int max { get; set; }
}
public class CreatePostDTO
{
    public string Email { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Category { get; set; }
    public string Img { get; set; }
}

public class CommentDTO
{
    public int PostId { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public string Date { get; set; }
    public string Avatar { get; set; }
}

public class MailDTO
{
    public string Receiver { get; set; }
    public string Sender { get; set; }
    public string Subject { get; set; }
    public string Mail_content { get; set; }
    public string ReceiverName { get; set; }
    public string SenderName { get; set; }
}