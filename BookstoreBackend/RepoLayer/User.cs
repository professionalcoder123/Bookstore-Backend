using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepoLayer
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="First name is required")]
        [RegularExpression("^[A-Za-z]+$",ErrorMessage ="First name should contain only letters")]
        [Column("FirstName",TypeName ="VARCHAR(20)")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last name is required")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Last name should contain only letters")]
        [Column("LastName",TypeName ="VARCHAR(20)")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Email address is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Role is required")]
        public string Role { get; set; }
    }
}
