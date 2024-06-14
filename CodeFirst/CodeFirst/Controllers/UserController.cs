using CodeFirst.Context;
using CodeFirst.Helpers;
using CodeFirst.Interfaces;
using CodeFirst.Models;
using CodeFirst.Models.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirst.Controllers;


[Route("api/client")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IClientRepository _clientRepository;
    private readonly Apbd9Context _dbContext;

    public UserController(IClientRepository clientRepository, Apbd9Context dbContext)
    {
        _clientRepository = clientRepository;
        _dbContext = dbContext;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterRequest model)
    {
        await _clientRepository.RegisterClientAsync(model);
        return Ok();
    }


}