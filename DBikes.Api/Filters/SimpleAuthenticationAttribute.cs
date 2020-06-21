using System;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace DBikes.Api.Filters
{
    public class SimpleAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
      public bool AllowMultiple {
            get{return false;}
        }

        public Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var bearerAuth = context.Request.Headers.Authorization;
            if (bearerAuth == null || bearerAuth.Scheme != "Bearer" || bearerAuth.Parameter != "eGFtYXJpbjpzdGF0aWN0ZXN0")
            {
                context.ErrorResult = new System.Web.Http.Results.UnauthorizedResult(
                    new AuthenticationHeaderValue[0],
                    context.Request
                    );
            }
            return Task.FromResult(0);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
    }
}