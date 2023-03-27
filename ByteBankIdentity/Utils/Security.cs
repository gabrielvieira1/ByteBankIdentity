using BCrypt.Net;

namespace ByteBankIdentity.Utils
{
 public class Security
 {
  public static string HashPassword(string password)
  {
   return BCrypt.Net.BCrypt.EnhancedHashPassword(password, 14);
  }
  public static bool VerifyHashedPassword(string password, string hashedPassword)
  {
   return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
  }
 }
}
