using System.Text.RegularExpressions;

namespace ByteBankIdentity.Utils
{
 public class String
 {
  public static bool InputIsValid(string input)
  {
   if (string.IsNullOrEmpty(input))
    return false;

   string pattern = @"^[A-Za-záàâãéèêíïóôõöúçñÁÀÂÃÉÈÍÏÓÔÕÖÚÇÑ ]+$";
   Regex rgx = new Regex(pattern);
   if (!rgx.IsMatch(input))
    return false;

   return true;
  }
  public static bool EmailIsValid(string input)
  {
   if (string.IsNullOrEmpty(input))
    return false;

   string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
   Regex rgx = new Regex(pattern);
   if (!rgx.IsMatch(input))
    return false;

   return true;
  }
 }
}
