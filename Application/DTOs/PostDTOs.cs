namespace API.DTOs;

public class PostDTO
{
    public string Email { get; set; }
    public string UserName { get; set; }
    public double Price { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Authority { get; set; }
    public string Address { get; set; }
    public int Category { get; set; }
}

public class CategoryDTO
{
    public int[] ids { get; set; }
}

public class PriceDTO
{
    public int min { get; set; }
    public int max { get; set; }
}

public class CatPriceDTO
{
    public int[] ids { get; set; }
    public int min { get; set; }
    public int max { get; set; }
}

public class SearchDTO
{
    public string args { get; set; }
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
}