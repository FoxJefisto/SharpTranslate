using System.ComponentModel.DataAnnotations;

namespace SharpTranslate.Models.DatabaseModels;

public enum UserWordStatus
{
    [Display(Name = "not_studied")]
    NotStudied,
    [Display(Name = "studying")]
    Studying,
    [Display(Name = "learned")]
    Learned
}
