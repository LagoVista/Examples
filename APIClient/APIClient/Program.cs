using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var clientId = "1243F889051245E1BE476B45D761E041";
            var apiToken = "bYsLggn7d7Z99K7vcaHsojE/LTK8cVqDjaay0WtmAsooZY80PHiyIl181hGzbMfCc3NNnD4vbPswiPRjSaZKFg==";

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("APIToken", $"{clientId}:{apiToken}");

            var response = await client.GetStringAsync("https://dev-api.nuviot.com/clientapi/devices");
            Console.WriteLine(response);

            Console.ReadKey();
        }
    }
}
