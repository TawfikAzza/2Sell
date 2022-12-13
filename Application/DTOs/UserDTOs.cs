namespace API.DTOs;


public class LoginDTO
{
    public string Email { get; set; }
    public string Password { get; set; }

}

public class RegisterDTO
{
    public string Email { get; set; }
    public string userName { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public string? Img { get; set; }
}

public class UserDTO
{
    public string Email { get; set; }
    public string userName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public string Img { get; set; }
}
