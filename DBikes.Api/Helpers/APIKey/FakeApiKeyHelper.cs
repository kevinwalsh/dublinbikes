
namespace DBikes.Api.Helpers.APIKey
{
    public class FakeApiKeyHelper:  IApiKeyHelper
    {
        public string GetApiKey(string app)
        {
            if (app.Equals("dublinbikes"))
            {
                return "fake-api-key";
            }
            else {
                return "";
            }

        }

    }
}