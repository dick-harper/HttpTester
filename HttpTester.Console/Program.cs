using System.Net.Http.Headers;
using System.Text.Json;

using HttpClient client = new();

client.DefaultRequestHeaders.Accept.Clear();

// accept JSON responses
client.DefaultRequestHeaders.Accept.Add(
  new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));

// necessary to retrieve information from GitHub
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

var repositories = await ProcessRepositoriesAsyncy(client);

foreach (var repo in repositories)
{
  Console.WriteLine($"Name: {repo.Name}");
  Console.WriteLine($"Homepage: {repo.Homepage}");
  Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
  Console.WriteLine($"Description: {repo.Description}");
  Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
  Console.WriteLine($"{repo.LastPush}");
  Console.WriteLine();
}

Console.ReadLine();

static async Task<List<Repository>> ProcessRepositoriesAsyncy(HttpClient client)
{
  await using Stream stream =
    await client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
  var repositories =
      await JsonSerializer.DeserializeAsync<List<Repository>>(stream);
  return repositories ?? new();
}