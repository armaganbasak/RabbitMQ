using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Send
{
    public class Encryption
    {
        public static byte[] EncryptAes(string plainText, string key)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(key);
                aesAlg.IV = new byte[16]; // 16 byte uzunluğunda bir IV


                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())//şifrelenmiş veriyi saklamak için MemoryStream
                {
                    //Veriyi şifrelemek için CryptoStream
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        //Şifrelenmiş veriyi herhangi bir yere yazmak için StreamWriter
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    //Saklanmakta olan şifrelenmiş veriyi byte dizisi şeklinde return eder
                    return msEncrypt.ToArray();
                }
            }
        }
    }
}
