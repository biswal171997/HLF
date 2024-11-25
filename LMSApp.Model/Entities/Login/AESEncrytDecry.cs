using System;
using System.Text;
using System.Security.Cryptography;

namespace ClientsideEncryption
{
       public class AESEncrytDecry
   {

       public static string DecryptStringAES(string cipherText)
       {
           try
           {
               var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
               var iv = Encoding.UTF8.GetBytes("8080808080808080");

               var encrypted = Convert.FromBase64String(cipherText);
               //byte[] encrypted = Encoding.ASCII.GetBytes(cipherText);
               var decriptedFromJavascript = DecryptStringFromBytes(encrypted, keybytes, iv);
               //return string.Format(decriptedFromJavascript);
               return decriptedFromJavascript.ToString();
           }
           catch (Exception e)
           {
               return e.Message;
           }
       }

       private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
       {
           // Check arguments.
           if (cipherText == null || cipherText.Length <= 0)
           {
               throw new ArgumentNullException("cipherText");
           }
           if (key == null || key.Length <= 0)
           {
               throw new ArgumentNullException("key");
           }
           if (iv == null || iv.Length <= 0)
           {
               throw new ArgumentNullException("key");
           }

           // Declare the string used to hold
           // the decrypted text.
           string plaintext = null;

           // Create an RijndaelManaged object
           // with the specified key and IV.
           using (var rijAlg = new RijndaelManaged())
           {
               //Settings
               rijAlg.Mode = CipherMode.CBC;
               rijAlg.Padding = PaddingMode.PKCS7;
               rijAlg.FeedbackSize = 128;

               rijAlg.Key = key;
               rijAlg.IV = iv;

               // Create a decrytor to perform the stream transform.
               var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
               try
               {
                   // Create the streams used for decryption.
                   using (var msDecrypt = new MemoryStream(cipherText))
                   {
                       using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                       {

                           using (var srDecrypt = new StreamReader(csDecrypt))
                           {
                               // Read the decrypted bytes from the decrypting stream
                               // and place them in a string.
                               plaintext = srDecrypt.ReadToEnd();

                           }

                       }
                   }
               }
               catch
               {
                   plaintext = "keyError";
               }
           }

           return plaintext;
       }
       public static string EncryptData(string toEncrypt)
        {
            if (!string.IsNullOrEmpty(toEncrypt))
            {
                try
                {
                    byte[] keyArray;
                    byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
                    string key = "!@#$%^&*()_+~";
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();

                    using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                    {
                        aesProvider.Key = keyArray;
                        aesProvider.Mode = CipherMode.ECB;
                        aesProvider.Padding = PaddingMode.PKCS7;

                        ICryptoTransform encryptor = aesProvider.CreateEncryptor();
                        byte[] resultArray = encryptor.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                        string encryptedData = Convert.ToBase64String(resultArray, 0, resultArray.Length);
                        return encryptedData;
                    }
                }
                catch (CryptographicException ex)
                {
                    // Log or handle the exception as needed
                    Console.WriteLine("Encryption error: " + ex.Message);
                    return ""; // or throw ex; to propagate the exception
                }
            }
            else
            {
                return "";
            }
        }
        public static string DecryptData(string cipherString)
        {
            if (!string.IsNullOrEmpty(cipherString))
            {
                try
                {
                    cipherString = cipherString.Replace(" ", "+");
                    byte[] keyArray;
                    byte[] toEncryptArray = Convert.FromBase64String(cipherString);
                    string key = "!@#$%^&*()_+~";
                    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                    keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                    hashmd5.Clear();

                    using (AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider())
                    {
                        aesProvider.Key = keyArray;
                        aesProvider.Mode = CipherMode.ECB;
                        aesProvider.Padding = PaddingMode.PKCS7;

                        ICryptoTransform decryptor = aesProvider.CreateDecryptor();
                        byte[] resultArray = decryptor.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                        string decryptedText = Encoding.UTF8.GetString(resultArray);
                        return decryptedText;
                    }
                }
                catch (CryptographicException ex)
                {
                    // Log or handle the exception as needed
                    Console.WriteLine("Decryption error: " + ex.Message);
                    return ""; // or throw ex; to propagate the exception
                }
            }
            if (cipherString != "")
            {
                cipherString = cipherString.Replace(" ", "+");
                byte[] keyArray;
                byte[] toEncryptArray = Convert.FromBase64String(cipherString);
                string key = "!@#$%^&*()_+~";
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
                using (AesCryptoServiceProvider tdes = new AesCryptoServiceProvider())
                {
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = tdes.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    tdes.Clear();
                    string decodedData = Encoding.UTF8.GetString(resultArray);
                    return decodedData;
                   
                }
            }
            else
            {
                return "";
            }
        }
       private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
       {
           // Check arguments.
           if (plainText == null || plainText.Length <= 0)
           {
               throw new ArgumentNullException("plainText");
           }
           if (key == null || key.Length <= 0)
           {
               throw new ArgumentNullException("key");
           }
           if (iv == null || iv.Length <= 0)
           {
               throw new ArgumentNullException("key");
           }
           byte[] encrypted;
           // Create a RijndaelManaged object
           // with the specified key and IV.
           using (var rijAlg = new RijndaelManaged())
           {
               rijAlg.Mode = CipherMode.CBC;
               rijAlg.Padding = PaddingMode.PKCS7;
               rijAlg.FeedbackSize = 128;

               rijAlg.Key = key;
               rijAlg.IV = iv;

               // Create a decrytor to perform the stream transform.
               var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

               // Create the streams used for encryption.
               using (var msEncrypt = new MemoryStream())
               {
                   using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                   {
                       using (var swEncrypt = new StreamWriter(csEncrypt))
                       {
                           //Write all data to the stream.
                           swEncrypt.Write(plainText);
                       }
                       encrypted = msEncrypt.ToArray();
                   }
               }
           }

           // Return the encrypted bytes from the memory stream.
           return encrypted;
       }

   }
}

