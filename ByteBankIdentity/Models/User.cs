using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ByteBankIdentity.Models
{
 public class User
 {
  [Key]
  public int Id { get; set; }
  [Required]
  [StringLength(200, ErrorMessage = "Name is required", MinimumLength = 8)]
  public string Name { get; set; }
  [Required]
  [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email")]
  [DisplayName("E-mail")]
  public string Email { get; set; }
  [Required]
  [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
  [DataType(DataType.Password)]
  public string Password { get; set; }
  [Required]
  public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
  [Required]
  public bool Active { get; set; }
 }
}