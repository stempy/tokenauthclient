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
        static void Main(string[] args)
        {
            using (var c = new TokenClient("http://localhost:56516"))
            {
                c.DefaultRequestHeaders.Add("TESTING", "LOCALHOST");
                c.DefaultRequestHeaders.Add("AppClient","AU");

                var authToken = c.AuthenticateAsync("TESTING", "dsdss").GetAwaiter().GetResult();
                Dump(authToken);
            }


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
