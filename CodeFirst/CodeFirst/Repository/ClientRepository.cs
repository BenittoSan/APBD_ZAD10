using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CodeFirst.Context;
using CodeFirst.Exceptions;
using CodeFirst.Helpers;
using CodeFirst.Interfaces;
using CodeFirst.Models;
using CodeFirst.Models.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace CodeFirst.Repository;

public class ClientRepository : IClientRepository
{
    private readonly Apbd9Context _dbContext;

    public ClientRepository(Apbd9Context dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<RegisterRequest?> RegisterClientAsync(RegisterRequest registerRequest)
    {
        var hashedPassworAndSalt = SecurityHelper.GetHashedPasswordAndSalt(registerRequest.Password);

        var user = new User
        {
            Login = registerRequest.Login,
            Password = hashedPassworAndSalt.Item1,
            Salt = hashedPassworAndSalt.Item2,
            Email = registerRequest.Email,
            RefreshToken = SecurityHelper.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return registerRequest;

    }

    public async Task<LoginRequest?> Login(LoginRequest loginRequest)
    {
        User user = await _dbContext.Users.Where(u => u.Login == loginRequest.Login).FirstOrDefaultAsync();

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelper.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            throw new AuthorizationException("Access denied");
        }


        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "beny"),
            new Claim(ClaimTypes.Role, "admin")
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tajnehasloniemownikomu"));
        SigningCredentials creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _dbContext.SaveChangesAsync();

        
        
        return loginRequest;

    }

}