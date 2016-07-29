using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TokenAuthClient
{
    public class TokenAuth
    {
        public async Task<TokenObj> RequestNewToken(string tokenServer, string username, string password)
        {
            using (var client = new TokenClient(tokenServer))
            {
                var result = await client.AuthenticateAsync(username, password);

            }

            return null;
        }
    }
}
