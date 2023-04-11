using System.Text.Json.Serialization;

public record class Repository(
    [property: JsonPropertyName("name")] string Name);