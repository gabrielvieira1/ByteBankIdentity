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
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public DateTime CreatedDateTime { get; set; } = DateTime.Now;
  }
}