using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TokenAuthClient
{
    public class BearerTokenAuthClientHandler : DelegatingHandler
    {
        private readonly string _authToken;

        public BearerTokenAuthClientHandler(string authToken)
        {
            _authToken=authToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            System.Threading.CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_authToken))
                throw new Exception("AuthToken not set");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            return await base.SendAsync(request,cancellationToken);
        }
    }
}
