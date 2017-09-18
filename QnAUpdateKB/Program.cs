using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QnAUpdateKB
{
    static class Program
    {
        static void Main()
        {
            MakeRequest();
            Console.ReadKey();
        }


        static async void MakeRequest()
        {
            const string KnowledgebaseId = "37ad2057-f948-4fe1-9b76-49580ab51e23";
            const string QnamakerSubscriptionKey = "d918c819513e4932b16ba18d4b0383c1";
            Uri UrlAddress = new Uri($"https://westus.api.cognitive.microsoft.com/qnamaker/v2.0/knowledgebases/{KnowledgebaseId}?");

            string answer = "Текст ответа";
            string question = "Текст вопроса";

            string stringData = "{\"add\": {\"qnaPairs\": [{\"answer\": \"" + answer + "\",\"question\": \"" + question + "\"}],\"urls\": []},\"delete\": {\"qnaPairs\": []}}";

            var method = new HttpMethod("PATCH");

            HttpContent content = new StringContent(stringData, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", QnamakerSubscriptionKey);

            var request = new HttpRequestMessage(method, UrlAddress)
            {
                Content = content
            };

            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                response = await client.SendAsync(request);
            }
            catch (TaskCanceledException e)
            {
                Debug.WriteLine("ERROR: " + e.ToString());
            }
        }
    }
}