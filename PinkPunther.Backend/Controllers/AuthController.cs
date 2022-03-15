using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace PinkPunther.Backend.Controllers;

[Route("/api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    [HttpGet("Login")]
    public IActionResult Login()
    {
        return Challenge(new AuthenticationProperties {RedirectUri = "/"});
    }

    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return Redirect("/");
    }
}