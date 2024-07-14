using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using RestSharp;

namespace apiChallengeLGC.Tests;
//Json Placeholder API does not create, update and delete resources but fakes them
//If those actions were real, these tests would have to account for that

[AllureNUnit]

[TestFixture]
[AllureFeature("Json placeholder API Tests")]
public class JsonPostsTests
{
    private RestClient? client;
    private PayloadBody? payload;

    [SetUp]
    public void Setup()
    {
        var options = new RestClientOptions
        {
            BaseUrl = new Uri("https://jsonplaceholder.typicode.com/"),
            MaxRedirects = 10,
            ThrowOnAnyError = false,
            FollowRedirects = true
        };

        client = new RestClient(options);
    }

    [TearDown]
    public void Teardown()
    {
        client?.Dispose();
    }

    [Test]
    [AllureStory("GET request for post 1 should return results")]
    public void GetPostOne_ShouldReturnResults()
    {
        var request = new RestRequest("posts/1", Method.Get);

        Console.WriteLine($"TEST NAME: GetPostOne_ShouldReturnResults");
        ValidationHelper.RequestResponseLogger.LogRequest(request);

        if (client == null) throw new ArgumentNullException(nameof(client));
        var response = client.Execute(request);

        ValidationHelper.RequestResponseLogger.LogResponse(response);
        ValidationHelper.ApiResponseValidator.ValidateApiResponse(response.Content, (int)response.StatusCode, 200, "GetOnePost.json");
    }

    [Test]
    [AllureStory("POST request should create a new post")]
    public void PostNewPost_ShouldCreateNewPost()
    {
        payload = new PayloadBody
        {
            Title = "Lorem ipsum",
            Body = "Aliquam eu purus eu velit viverra ornare in quis velit.",
            UserId = 1
        };

        var request = new RestRequest("posts", Method.Post);
        request.AddJsonBody(payload);

        Console.WriteLine($"TEST NAME: PostNewPost_ShouldCreateNewPost");
        ValidationHelper.RequestResponseLogger.LogRequest(request);

        if (client == null) throw new ArgumentNullException(nameof(client));
        var response = client.Execute(request);

        ValidationHelper.RequestResponseLogger.LogResponse(response);
        ValidationHelper.ApiResponseValidator.ValidateApiResponse(response.Content, (int)response.StatusCode, 201, "PostLoremIpsumPost.json");
    }

    [Test]
    [AllureStory("PUT request should update an existing post")]
    public void UpdatePost_ShouldUpdateExistingPost()
    {
        payload = new PayloadBody
        {
            Title = "Vestibulum molestie",
            Body = "Morbi fringilla consequat leo sed ultricies.",
            UserId = 1,
            Id = 1
        };

        var request = new RestRequest("posts/1", Method.Put);
        request.AddJsonBody(payload);

        Console.WriteLine($"TEST NAME: UpdatePost_ShouldUpdateExistingPost");

        ValidationHelper.RequestResponseLogger.LogRequest(request);

        if (client == null) throw new ArgumentNullException(nameof(client));
        var response = client.Execute(request);

        ValidationHelper.RequestResponseLogger.LogResponse(response);
        ValidationHelper.ApiResponseValidator.ValidateApiResponse(response.Content, (int)response.StatusCode, 200, "UpdatePost.json");
    }

    [Test]
    [AllureStory("DELETE request should delete an existing post")]
    public void DeletePostOne_ShouldReturnSuccess()
    {
        var request = new RestRequest("posts/1", Method.Delete);

        Console.WriteLine($"TEST NAME: DeletePostOne_ShouldReturnSuccess");
        ValidationHelper.RequestResponseLogger.LogRequest(request);

        if (client == null) throw new ArgumentNullException(nameof(client));
        var response = client.Execute(request);

        ValidationHelper.RequestResponseLogger.LogResponse(response);
        Assert.That((int)response.StatusCode, Is.EqualTo(200), $"Expected status code {200}, but received {response.StatusCode}");
    }
}
