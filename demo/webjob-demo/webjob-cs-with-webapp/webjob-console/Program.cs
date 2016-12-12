using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace webjob_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = MakeRequest();
            task.Wait();

            var response = task.Result;
            var body = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(body);

        }

        private static async Task<HttpResponseMessage> MakeRequest()
        {
            var httpClient = new HttpClient();
            // 자신의 URL로 변경 필요
            return await httpClient.GetAsync(new Uri("http://requestb.in/1jdmoz91"));
        }

    }
}
