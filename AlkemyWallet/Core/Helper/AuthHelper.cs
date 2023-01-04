using System.Text;

namespace AlkemyWallet.Core.Helper
{
    public class AuthHelper
    {
        public static string EncryptPassword(string password)
        {
            string salt = "BootNetLaDotneta2022";
            password += salt;
            byte[] encoded = Encoding.UTF8.GetBytes(password);
            string encrypted = Convert.ToBase64String(encoded);
            return encrypted;
        }
    }
}
