using System.Net.Http.Headers;
using System.Text.Json;

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
  await using Stream stream =
    await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
  var repositories =
      await JsonSerializer.DeserializeAsync<List<Repository>>(stream);

  foreach (var repo in repositories ?? Enumerable.Empty<Repository>())
    Console.WriteLine(repo.Name);
}