namespace SharpTranslate.Models
{
    public class WordRequestResponse
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public int UserWordPairId { get; set; }

        public string? SourceWord { get; set; }

        public string? SourceLanguage { get; set; }

        public string? TargetWord { get; set; }
        
        public string? TargetLanguage { get; set; }
    }
}
