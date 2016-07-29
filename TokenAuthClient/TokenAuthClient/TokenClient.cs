﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace TokenAuthClient
{
    public class TokenObj
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
    }


    public class TokenClient : HttpClient
    {
        public string TokenServer { get; set; }
        public string TokenAuthAction { get; set; } = "token";

        public dynamic AuthTokenResult { get; private set; }




        #region [Ctors..]

        public TokenClient() : base(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
        })
        {
        }

        public TokenClient(string tokenServer) : base(new HttpClientHandler()
        {
            AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
        })
        {
            TokenServer = tokenServer;
        }

        public TokenClient(HttpMessageHandler handler) : base(handler)
        {
        }

        public TokenClient(HttpMessageHandler handler, string tokenServier) : base(handler)
        {
            TokenServer = tokenServier;
        }

        #endregion

        public async Task<TokenObj> AuthenticateAsync(string username, string password)
        {
            var content = new FormUrlEncodedContent(new[]
               {
                    new KeyValuePair<string, string>("grant_type","password"),
                    new KeyValuePair<string, string>("username",username),
                    new KeyValuePair<string, string>("password",password),
                });

            var url = $"{TokenServer}/{TokenAuthAction}";

            if (!this.DefaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
            {
                this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            

            var resp = await this.PostAsync(url, content);

            if (resp.IsSuccessStatusCode)
            {
                AuthTokenResult = await resp.Content.ReadAsAsync<TokenObj>();
                return AuthTokenResult;
            }
            else
            {
                var cnt = await resp.Content.ReadAsStringAsync();
                var exMsg = $"StatusCode:{resp.StatusCode} - {resp.ReasonPhrase} - {cnt}";
                AuthTokenResult = new {Content = cnt, Message=exMsg};
                throw new AuthenticationException($"Unable to authenticate with {url} - Message:\n{exMsg}");
            }
        }

    }
}