using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniChat_BLL.Dto;
using UniChat_BLL;

namespace Wildlife_BackEnd.Controllers;


[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public AuthController(UserService userService, AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        UserDto? user = _userService.GetUserByUsername(loginDto.Username);
        if (user == null || !_userService.VerifyPassword(loginDto.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials");

        var accessToken = _authService.GenerateAccessToken(user);
        var refreshToken = _authService.GenerateRefreshToken();
        var refreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        _userService.UpdateRefreshToken(user.Id, refreshToken, refreshTokenExpiry);

        Response.Cookies.Append("jwt", accessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        return Ok(new { message = "Login successful", refreshToken });
    }




    [HttpPost("refresh")]
    public IActionResult RefreshToken([FromBody] string refreshToken)
    {
        UserDto? user = _userService.GetUserByRefreshToken(refreshToken);
        if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            return Unauthorized("Invalid refresh token");

        var newAccessToken = _authService.GenerateAccessToken(user);
        var newRefreshToken = _authService.GenerateRefreshToken();
        var newRefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

        // Update refresh token
        _userService.UpdateRefreshToken(user.Id, newRefreshToken, newRefreshTokenExpiry);

        // Set new JWT in HttpOnly cookie
        Response.Cookies.Append("jwt", newAccessToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        return Ok(new { refreshToken = newRefreshToken });
    }



}

