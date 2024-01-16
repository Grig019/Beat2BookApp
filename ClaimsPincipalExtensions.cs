using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Beat2Book
{
    public static class ClaimsPincipalExtensions
    { 
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value; 
        }
    }
}
