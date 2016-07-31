using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TokenAuthClient
{
    public class BearerTokenAuthClientHandler : DelegatingHandler
    {
        private readonly TokenObj _authToken;

        public BearerTokenAuthClientHandler(TokenObj tokenObj)
        {
            _authToken=tokenObj;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            System.Threading.CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_authToken.access_token))
                throw new Exception("AuthToken not set");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authToken.access_token);
            return await base.SendAsync(request,cancellationToken);
        }
    }
}
