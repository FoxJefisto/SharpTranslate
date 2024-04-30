using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpTranslate.Models.DatabaseModels
{
    [Table("word")]
    public class Word
    {
        [Key]
        public int Id { get; set; }

        public string? WordName { get; set; }

        public string? Language { get; set; }
    }
}
