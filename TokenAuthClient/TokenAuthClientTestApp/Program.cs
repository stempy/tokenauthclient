using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TokenAuthClient;

namespace TokenAuthClientTestApp
{
    class Program
    {

        private static async Task DoTest()
        {
            var host = @"http://localhost:56516";

            // with auth
            using (var c = new TokenClient(host))
            {
                c.DefaultRequestHeaders.Add("TESTING", "LOCALHOST");
                Console.WriteLine("Testing with AUTH");
                
                // Get auth token
                var authToken = await c.AuthenticateAsync("TESTING", "dsdss");
                await TestBasicAuthoriseRequest(c,host);
                //Dump(authToken);
            }

            // test without authorise
            using (var c = new TokenClient(host))
            {
                c.DefaultRequestHeaders.Add("TESTING", "LOCALHOST");
                Console.WriteLine("Testing with NO AUTH");
                await TestBasicAuthoriseRequest(c,host);
            }
        }



        private static async Task TestBasicAuthoriseRequest(TokenClient c, string host)
        {
            // now test token is working on method
            var getTest = await c.GetAsync($"{host}/test/token");
            if (getTest.IsSuccessStatusCode)
            {
                var content = await getTest.Content.ReadAsStringAsync();
                Console.WriteLine("Success"+content);
            }
            else
            {
                var msg = $"{getTest.StatusCode} - {getTest.ReasonPhrase}";
                Console.WriteLine("Error"+msg);
            }
        }


        static void Main(string[] args)
        {
            DoTest().GetAwaiter().GetResult();

            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }

        static void Dump(object o)
        {
            var json = JsonConvert.SerializeObject(o, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            Console.WriteLine(json);
        }
    }
}
