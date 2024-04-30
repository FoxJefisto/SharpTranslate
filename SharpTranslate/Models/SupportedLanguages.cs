namespace SharpTranslate.Models;

/// <summary>
/// The model for the supported languages api
/// </summary>
public class SupportedLanguages
{
    /// <summary>
    /// The code of the language
    /// </summary>
    public string? Code { get; set; }
    /// <summary>
    /// The english based language name
    /// </summary>
    public string? Name { get; set; }
}
