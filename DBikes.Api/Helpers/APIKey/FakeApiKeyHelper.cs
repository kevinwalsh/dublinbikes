
namespace DBikes.Api.Helpers.APIKey
{
    public class FakeApiKeyHelper:  IApiKeyHelper
    {
        public string GetApiKey(string app)
        {
            //return null;       // just always return null, let controller deal with it
            throw new System.Exception("KW APIKeyException: No API Key found");
                                // Better: fail fast & throw exception to alert user
        }

    }
}