using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpTranslate.Models.DatabaseModels
{
    [Table("user_wordpair")]
    public class UserWordPair
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [ForeignKey("WordPair")]
        public int WordPairId { get; set; }

        public virtual User? User { get; set; }

        public virtual WordPair? WordPair { get; set; }

        [EnumDataType(typeof(UserWordStatus))]
        public UserWordStatus WordStatus { get; set; }

    }
}
