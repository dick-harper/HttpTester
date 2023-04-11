using System.Net.Http.Headers;

using HttpClient client = new();

client.DefaultRequestHeaders.Accept.Clear();

// accept JSON responses
client.DefaultRequestHeaders.Accept.Add(
  new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

// necessary to retrieve information from GitHub
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

await ProcessRepositoriesAsyncy(client);

static async Task ProcessRepositoriesAsyncy(HttpClient client)
{
  var json = await client.GetStringAsync(
         "https://api.github.com/orgs/dotnet/repos");

  Console.Write(json);
}