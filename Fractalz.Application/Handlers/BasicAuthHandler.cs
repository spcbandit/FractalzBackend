//using System;
//using System.Diagnostics.CodeAnalysis;
//using System.Linq;
//using System.Net;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Encodings.Web;
//using System.Threading.Tasks;
//using Fractalz.Application.Abstractions;
//using Fractalz.Application.Domains;
//using Fractalz.Application.Domains.Responses.User;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;

//namespace Fractalz.Application.Handlers
//{
//    public class BasicAuthHandler : AuthenticationHandler<BasicAuthOptions>
//    {
//        private readonly IRepository<Domains.Entities.Profile.User> _repositoryUser;

//        public BasicAuthHandler(IRepository<Domains.Entities.Profile.User> repository,
//            IOptionsMonitor<BasicAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
//            : base(options, logger, encoder, clock)
//        {
//            _repositoryUser = repository ?? throw new ArgumentException(nameof(repository));
//        }

//        private string GetToken()
//        {
//            if (string.IsNullOrEmpty(Request.Headers["FX_Authorization"]))
//            {
//                string value = Request.Headers["Cookie"];
//                if (value.Contains("FX_Authorization"))
//                    return value.Split("FX_Authorization=")[1];
//                else
//                    return null;
//            }
//            else
//            {
//                return Request.Headers["FX_Authorization"];
//            }
//        }

//        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//        {
//            try
//            {
//                string authHandler = GetToken();


//                if (authHandler == null)
//                {
//                    return AuthenticateResult.Fail("Not found Authorization header");
//                }

//                var authHeaderValue = AuthenticationHeaderValue.Parse(authHandler);
//                if (!authHeaderValue.Scheme.Equals(AuthenticationSchemes.Basic.ToString(),
//                        StringComparison.OrdinalIgnoreCase))
//                {
//                    return AuthenticateResult.Fail("Wrong Authentication scheme");
//                }

//                var credentials = Encoding.UTF8
//                    .GetString(Convert.FromBase64String(authHeaderValue.Parameter ?? string.Empty))
//                    .Split(':', 2);

//                if (credentials.Length != 2)
//                {
//                    return AuthenticateResult.Fail("Incorrect credentials");
//                }

//                if (!IsAuthorized(credentials[0], credentials[1], out var id))
//                {
//                    return AuthenticateResult.Fail("Unrecognized client");
//                }

//                var claims = new[] { new Claim(ClaimTypes.Name, id.ToString()) };
//                var identity = new ClaimsIdentity(claims);
//                var principal = new ClaimsPrincipal(identity);

//                Context.User = principal;
//                return AuthenticateResult.Success(new AuthenticationTicket(principal, "BasicScheme"));
//            }
//            catch (Exception exception)
//            {
//                return AuthenticateResult.Fail(exception);
//            }
//        }

//        private bool IsAuthorized(string username, string password, out int id)
//        {
//            id = 0;
//            if (_repositoryUser.Get(x => x.Login == username || username == x.Email).FirstOrDefault() != null)
//            {
//                var user = _repositoryUser
//                                .Get(x => (x.Login == username || x.Email == username) && x.Password == password)
//                                .FirstOrDefault();

//                if (user != null)
//                {
//                    id = user.Id;
//                    return true;
//                }

//                return false;
//            }
//            else
//            { return false; }
//        }
//    }
//}