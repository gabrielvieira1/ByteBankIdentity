using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ByteBankIdentity.Models
{
 public class User
 {
  [Key]
  public int Id { get; set; }
  [Required]
  public string Name { get; set; }
  [Required]
  [DisplayName("E-mail")]
  public string Email { get; set; }
  [Required]
  public string Password { get; set; }
  [Required]
  public DateTime CreatedDateTime { get; set; } = DateTime.Now;
  [Required]
  public bool Active { get; set; }
 }
}