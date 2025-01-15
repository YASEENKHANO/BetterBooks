using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BetterBooks.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "The Order must be between 1 and 100")]
        public int DisplayOrder { get; set; }
        //Checking username/email for commit only
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;//current date time
    }
}
