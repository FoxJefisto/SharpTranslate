using System.Text.Json.Serialization;

namespace SharpTranslate.Models;

/// <summary>
/// The model for the translation api response
/// </summary>
public class TranslationResponse
{
    /// <summary>
    /// The translated text
    /// </summary>
    [JsonPropertyName("translatedText")]
    public string? TranslatedText { get; set; }
}
