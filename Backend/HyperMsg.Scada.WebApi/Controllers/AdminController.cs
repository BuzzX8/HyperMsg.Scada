using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HyperMsg.Scada.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    #region Users

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _userManager.Users.ToList();

        return Ok(users);
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser(string userName, string password)
    {
        var user = new IdentityUser { UserName = userName, Email = userName };
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            return Ok("User created successfully.");
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    [HttpPut("user/{id}")]
    public async Task<IActionResult> UpdateUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        //user.UserName = newUserName;
        //user.Email = newUserName;
        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Ok("User updated successfully.");
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }

    #endregion

    #region Permissions

    [HttpPost("user/{id}/permissions")]
    public async Task<IActionResult> AddPermissionsToUser(string id, [FromBody] List<string> permissions)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound("User not found.");
        }
        var userClaims = await _userManager.GetClaimsAsync(user);
        foreach (var permission in permissions)
        {
            if (!userClaims.Any(c => c.Type == "Permission" && c.Value == permission))
            {
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Permission", permission));
            }
        }
        return Ok("Permissions added successfully.");
    }

    #endregion
}
