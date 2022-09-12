using System.Security.Claims;

public static class IdentityExtension
{
    public static string GetId(this ClaimsPrincipal principal)
    {
        try
        {
            return new (principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? string.Empty);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string GetName(this ClaimsPrincipal principal)
    {
        try
        {
            return principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string GetEmail(this ClaimsPrincipal principal)
    {
        try
        {
            return principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}