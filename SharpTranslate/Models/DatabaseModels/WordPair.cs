using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpTranslate.Models.DatabaseModels
{
    [Table("wordpair")]
    public class WordPair
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OriginalWord")]
        public int OriginalWordId { get; set; }

        [ForeignKey("TranslationWord")]
        public int TranslationWordId { get; set; }

        public virtual Word? OriginalWord { get; set; }

        public virtual Word? TranslationWord { get; set; }
    }
}
