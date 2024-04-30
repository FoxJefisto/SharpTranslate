using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SharpTranslate.Models.DatabaseModels
{
    [Table("user")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? UserName { get; set; }

        public virtual ICollection<UserWordPair?>? UserWords { get; set; }
    }
}
