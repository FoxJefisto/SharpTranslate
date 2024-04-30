using System.Text.Json.Serialization;

namespace SharpTranslate.Models;

/// <summary>
/// The model to send to the libre translate api
/// </summary>
public class Translate
{
    /// <summary>
    /// The text to be translated
    /// </summary>
    [JsonPropertyName("q")]
    public string? Text { get; set; }
    /// <summary>
    /// The source of the current language text
    /// </summary>
    [JsonPropertyName("source")]
    public string? SourceLanguage { get; set; }
    /// <summary>
    /// The target of the language we want to convert text
    /// </summary>
    [JsonPropertyName("target")]
    public string? TargetLanguage { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; } = "text";
}
