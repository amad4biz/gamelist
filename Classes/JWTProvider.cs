using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using OpenGameList.Data;
using OpenGameList.Data.Users;

namespace OpenGameList.Classes
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JWTProvider
    {


        #region private members
        private readonly RequestDelegate _next;
        // JWT-related members
        private TimeSpan TokenExpiration;
        private SigningCredentials SigningCredentials;
        // EF and Identity members, available through DI
        private ApplicationDbContext DbContext;
        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;

        #endregion Private Members

        #region Static Members
        private static readonly string PrivateKey = "private_key_1234567890";
        public static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey));
        public static readonly string Issuer = "OpenGameList";
        public static string TokenEndPoint = "/api/connect/token";
        #endregion Static Members

        public JWTProvider(RequestDelegate next,
             ApplicationDbContext dbContext,
             UserManager<ApplicationUser> userManager,
             SignInManager<ApplicationUser> signInManager)
        {
            _next = next;

            // Instantiate JWT-related members
            TokenExpiration = TimeSpan.FromMinutes(10);
            SigningCredentials = new SigningCredentials(SecurityKey,
           SecurityAlgorithms.HmacSha256);
            // Instantiate through Dependency Injection
            DbContext = dbContext;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JWTProvider>();
        }
    }
}
