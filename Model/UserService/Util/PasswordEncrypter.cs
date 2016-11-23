using System;
using System.Text;
using System.Security.Cryptography;


namespace Es.Udc.DotNet.PracticaMaD.Model.UserService
{
    /// <summary>
    /// Static Class with cryptografic utilities
    /// </summary>
    public static class PasswordEncrypter
    {
        /// <summary>
        /// Method to encrypt with a SHA 256 Algorithm
        /// </summary>
        /// <param name="password">String to encrypt</param>
        /// <returns>Returns a String with the <paramref name="password"/> encrypted
        /// </returns>                     

        public static String Crypt(String password)
        {

            HashAlgorithm hashAlg = new SHA256Managed();
            String encryptedPassword = "";
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                byte[] encryptedPasswordBytes = hashAlg.ComputeHash(passwordBytes);


                encryptedPassword = Convert.ToBase64String(encryptedPasswordBytes);
            }
            catch (System.ArgumentNullException) { }
            catch (System.Text.EncoderFallbackException) { }
            catch (System.ObjectDisposedException) { }
            finally
            {
               hashAlg.Dispose();
            }

            return encryptedPassword;
        }


        public static Boolean IsClearPasswordCorrect(String clearPassword,
            String encryptedPassword)
        {
            string encryptedClearPassword = Crypt(clearPassword);

            return encryptedClearPassword.Equals(encryptedPassword);
        }
    }
}
