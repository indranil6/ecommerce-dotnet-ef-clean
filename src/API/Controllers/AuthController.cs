using System;
using System.Security.Claims;
using Application.Auth.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterUser.Command command)
    {
        var result = await Mediator.Send(command);
        return Ok(new
        {
            Message = "User registered successfully",
            Details = result
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUser.Command command)
    {
        var token = await Mediator.Send(command);

        Response.Cookies.Append("accessToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = false, // Set to true in production
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddHours(1)
        });
        if (token == null)
        {
            return Unauthorized(new
            {
                Message = "Invalid username or password"
            });
        }
        return Ok(new
        {
            Message = "Login successful",
            Token = token
        });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("accessToken");
        return Ok(new
        {
            Message = "Logout successful"
        });
    }
}
