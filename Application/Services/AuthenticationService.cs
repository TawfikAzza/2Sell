using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Services;
using Application.Helpers;
using Application.Interfaces;
using Core;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppSettings _appSettings;
    private readonly IUserRepository _userRepository;
    private readonly IBikeShopRepository _bikeShopRepository;
    private byte[] _secretByte;
    public AuthenticationService(IUserRepository repository
        ,IOptions<AppSettings> appSettings
        ,IBikeShopRepository bikeShopRepository)
    {
        _userRepository = repository;
        _appSettings = appSettings.Value;
        _bikeShopRepository = bikeShopRepository;

    }
    public bool ValidateUser(string userName, string password, out string token)
    {
        throw new NotImplementedException();
    }

    public string Register(RegisterDTO dto)
    {
         try
        {
            _userRepository.GetUserByEmail(dto.Email);
        }
        catch (KeyNotFoundException)
        {
            var saltBytes = RandomNumberGenerator.GetBytes(32);
            string salt = "";
            foreach (byte bit in saltBytes)
            {
                salt += bit;
            }

            
            Console.WriteLine("Salt: "+salt+" Email: "+dto.Email+" password : "+dto.Password);
            User user = new User() {
                Email = dto.Email,
                Salt = salt,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password + salt),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                PostalCode = dto.PostalCode
            };
            
            _userRepository.CreateNewUser(user);
            return GenerateToken(user);
        }

        throw new Exception("Email " + dto.Email + " is already in use");
    }

    public string Login(LoginDTO dto)
    {
        var user = _userRepository.GetUserByEmail(dto.Email);
        if (BCrypt.Net.BCrypt.Verify(dto.Password + user.Salt, user.PasswordHash))
        {
            return GenerateToken(user);
        }

        throw new Exception("Invalid Login");
    }
    
    //Peter's way of implementing the token generation, I paste it here if you want to have a look at how he does it,
    //I decided in the end to use Alex's approach as it is more clear and straightforward and contains everything we need to know
    //about token creation for now.
    private string CreateToken(string username)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
        //claims.Add(new Claim(ClaimTypes.Role, _userRole[username]));
        //byte[] secret = System.Text.Encoding.UTF8.GetBytes("SuperDuperSecret");
        JwtSecurityToken token = new JwtSecurityToken(
            new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(_secretByte),
                SecurityAlgorithms.HmacSha512)),
            new JwtPayload(null, null,claims,DateTime.Now,DateTime.Now.AddMinutes(10))
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    private string GenerateToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
                { new Claim("email", user.Email), new Claim("Role", "Owner") }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}