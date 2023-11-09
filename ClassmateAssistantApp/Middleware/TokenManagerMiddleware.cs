using BusinessLayer.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;

namespace HouseBooking.Middleware
{
    public class TokenManagerMiddleware //: IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtOptions _options;


        /// <summary>
        /// TokenManagerMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="options"></param>
        public TokenManagerMiddleware(RequestDelegate next, IOptions<JwtOptions> options)
        {
            _next = next;
            _options = options.Value;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context, IApplicationUserService userService)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

            if (!string.IsNullOrEmpty(token) && !AttachUserToContext(context, userService, token))
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            else
                await _next(context);
                return;
        }


        private bool AttachUserToContext(HttpContext context, IApplicationUserService userService, string token)
        {

            bool isValid = false;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_options.Key);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = _options.ValidateLifetime,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == "id").Value;
                var accessToken = jwtToken.Claims.First(x => x.Type == "access").Value;


                var user = userService.GetById(userId);

                if (user != null && user.AccessToken!.Equals(accessToken))
                {
                    // attach user to context on successful jwt validation
                    context.Items["User"] = user;
                    isValid = true;
                }
                else if (user != null && !user.AccessToken!.Equals(accessToken))
                    isValid = false;

            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }

            return isValid;
        }
    }
}
