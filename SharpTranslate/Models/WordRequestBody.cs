using SharpTranslate.Models.DatabaseModels;

namespace SharpTranslate.Models
{
    public class WordRequestBody
    {
        public int UserId { get; set; }

        public string? Word { get; set; }

        public string? SourceLanguage { get; set; }

        public string? TargetLanguage { get; set; }

        public string? TargetWord { get; set; }

        public UserWordStatus UserWordStatus { get; set; }
    }
}
