using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;

namespace ValidationHelper
{
    public static class ApiResponseValidator
    {
        public static void ValidateApiResponse(string? responseBody, int responseStatusCode, int expectedStatusCode, string schemaFileName)
        {
            // Get the Json schema
            var schemaPath = Path.Combine("../../../JsonSchemas", schemaFileName);
            var schemaJson = File.ReadAllText(schemaPath);
            var schema = JSchema.Parse(schemaJson);

            responseBody ??= "";

            var content = JObject.Parse(responseBody);

            // Validate the response against the schema and print errors if any
            bool isValid = content.IsValid(schema, out IList<string> errorMessages);

            if (isValid)
            {
                Console.WriteLine($"Validation successful for schema ({schemaFileName}).");
            }
            {
                foreach (var error in errorMessages)
                {
                    Console.WriteLine("Validation Error: " + error);
                }
            }

            // Assert that the JSON response is valid according to the schema and the status is the one expected
            Assert.That(responseStatusCode, Is.EqualTo(expectedStatusCode), $"Expected status code {expectedStatusCode}, but received {responseStatusCode}");
            Assert.That(isValid, Is.True, $"JSON response does not match schema ({schemaFileName}).");
        }
    }

    public static class RequestResponseLogger
    {
        public static void LogRequest(RestRequest request)
        {
            Console.WriteLine("REQUEST TO: " + request.Resource);
            Console.WriteLine("REQUEST METHOD: " + request.Method);
            var bodyParameter = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
            if (bodyParameter != null)
            {
                string jsonPayload = JsonConvert.SerializeObject(bodyParameter.Value, Formatting.Indented);
                Console.WriteLine("REQUEST JSON PAYLOAD: " + jsonPayload);
            }
        }
        public static void LogResponse(RestResponse response)
        {
            Console.WriteLine("RESPONSE STATUS CODE (ENUM): " + (int)response.StatusCode);
            Console.WriteLine("RESPONSE STATUS CODE: " + response.StatusCode);
            Console.WriteLine("RESPONSE BODY: " + response.Content);
        }
    }

}
