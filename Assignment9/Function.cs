using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Dynamic;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Assignment9
{
    public class Function
    {
        
        public async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            string apiKey = "p7ZjMYscB8dYETiVX3Om4e4VfmiQptWe";
            //string apiSecret = "MsqkNKKqhV0yFaKm";

            HttpClient client = new HttpClient();
            dynamic obj = new ExpandoObject();
            HttpResponseMessage response = client.GetAsync("https://api.nytimes.com/svc/books/v3/lists/current/" + input.QueryStringParameters["list"] + ".json?api-key=" + apiKey).Result;
            response.EnsureSuccessStatusCode();
            string result = response.Content.ReadAsStringAsync().Result;
          
            var expConverter = new Newtonsoft.Json.Converters.ExpandoObjectConverter();
            obj = JsonConvert.DeserializeObject<ExpandoObject>(result, expConverter);
               
            return obj;
            

        }
    }
}
