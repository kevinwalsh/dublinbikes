using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;

namespace DBikes.Api.Filters
{
    public class SimpleAuthenticationAttribute : ActionFilterAttribute, IActionFilter
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var c = GenerateAuthenticationToken();
            StringValues bearerAuth;
            context.HttpContext.Request.Headers.TryGetValue("Authorization", out bearerAuth);

            if (bearerAuth.ToString() == null || bearerAuth != "Bearer " + c)

            {
                context.HttpContext.Response.StatusCode = 403;          // doesnt cancel controller action 
                context.Result = new ContentResult { Content = "403" };             // cancels controller action
            }

        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        /*      Verify/compute Authentication token 
           TASK: generate a simple but dynamic token by combining a static keyword + dynamic date, 
                  and hashing the result for obfustication; the hash will change daily
           PURPOSE: to generate a (simple) dynamic token to limit random API calls, 
                  to those who know the hashing algorithm format.
           SHORTCOMINGS: 
              - token is only regenerated periodically, so can be taken from a valid call & reused elsewhere for a limited time
              - minor chance of HTTP time delay resulting in different hashes. 
                    assuming 500ms delay, for 10^6 ms hash refresh, chance of failure is 0.05%.
              - Key & function is hardcoded on client & can be derived, but is at least hidden in outgoing HTTP header.
                  Neither key nor target API data is sensitive, but this obfustication will at least require 
                  extra effort from intruders to gain prolonged access. API will be protected from web scrapers.
          */
        public string GenerateAuthenticationToken()
        {
            var key = "dublinbikestoken";
            //var chararray = (key + DateTime.Today.ToString("yyyy-MM-dd")).ToCharArray();  //stringify todays date
            var chararray = (key + ((DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()) / 1000000));        // round to 15mins
            Int32 hash3 = 0;        // client javascript uses int32 default; overflows common; must use same here
            for (var i = 0; i < chararray.Length; i++)
            {
                hash3 = ((hash3 << 5) - hash3) + chararray[i];
                hash3 |= 0;              //  (convert to 32bit int)  https://stackoverflow.com/questions/7616461/generate-a-hash-from-string-in-javascript
            }
            return hash3.ToString();
        }

    }
}