using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;

namespace ConsoleApplication1
{
    class Program
    {

        private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            ProcessRepositories().Wait();
        }
        private static async Task ProcessRepositories()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", "KRK");
            
            var serializer = new DataContractJsonSerializer(typeof(List<repo>));
            var streamTask = client.GetStreamAsync("https://api.github.com/users/kkaminker/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<repo>;
            foreach(var rep in repositories)
            {
                Console.WriteLine(rep.name);
            }
            Console.ReadKey();
     
        }
    }
}
