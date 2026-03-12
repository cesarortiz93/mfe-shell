using MFE.Shared.Auth;
using Microsoft.AspNetCore.Components;

namespace Shell.Blazor.Services;

public class AuthService
{
    private readonly JwtTokenService _jwtService;
    private readonly IHttpContextAccessor _httpContext;
    private readonly NavigationManager _nav;

    public AuthService(
        JwtTokenService jwtService,
        IHttpContextAccessor httpContext,
        NavigationManager nav)
    {
        _jwtService = jwtService;
        _httpContext = httpContext;
        _nav = nav;
    }

    // Genera el JWT y lo guarda en cookie compartida
    public void Login(string userId, string email,
                      IEnumerable<string> roles)
    {
        var token = _jwtService.GenerateToken(userId, email, roles);

        _httpContext.HttpContext!.Response.Cookies.Append(
            "mfe_auth_token",
            token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // true en producción con HTTPS
                SameSite = SameSiteMode.Lax,
                Expires = DateTimeOffset.UtcNow.AddHours(1)
            });
    }

    // Elimina la cookie y redirige al login
    public void Logout()
    {
        _httpContext.HttpContext!.Response.Cookies
            .Delete("mfe_auth_token");
        _nav.NavigateTo("/login", forceLoad: true);
    }
}
