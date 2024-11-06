using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace AuthHandler
{
    public class AuthHandler:AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder) : base(options, logger, encoder)
        {
            
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Получаем токен из заголовка
            if (!Request.Headers.TryGetValue("Authorization", out StringValues tokenHeader))
            {
                return AuthenticateResult.NoResult();
            }

            // Извлекаем чистый токен без префикса "Bearer "
            string token = tokenHeader.ToString().Substring("Bearer ".Length);

            //Создаём запрос с токеном
            HttpRequestMessage httpRequest = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://localhost:7070/Authentication/"), 
            };
            httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            // Аутентификация через микросервис
            HttpResponseMessage response = await new HttpClient().SendAsync(httpRequest);

            if (response.IsSuccessStatusCode)
            {
                
                // Парсим JWT и извлекаем Claims
                JwtSecurityToken jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                Claim[] claims = jwtSecurityToken.Claims.ToArray();
                var identity = new ClaimsIdentity(claims, nameof(AuthHandler));
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }

            return AuthenticateResult.Fail("Invalid token");

        }

    }
}
