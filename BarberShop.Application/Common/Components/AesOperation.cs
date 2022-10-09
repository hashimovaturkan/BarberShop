using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BarberShop.Application.Common.Components
{
    public static class AesOperation
    {
        const string securityKey = "barbershop-app-2022-code-hash";

        //hashing algorithms
        public static string ToMd5(this string text)
        {
            using (var provider = new MD5CryptoServiceProvider())
            {
                byte[] textBuffer = Encoding.UTF8.GetBytes(text);
                byte[] hashedBuffer = provider.ComputeHash(textBuffer);

                StringBuilder sb = new StringBuilder();

                foreach (var hashedByte in hashedBuffer)
                {
                    sb.Append(hashedByte.ToString("x2"));
                }

                return sb.ToString();
            }

        }


        //symmetric algorithms
        public static string Encrypt(this string value, string key)
        {
            try
            {
                using (var provider = new TripleDESCryptoServiceProvider())
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    var ivBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"@{key}$"));
                    var keyBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"#{key}!"));

                    var transform = provider.CreateEncryptor(keyBuffer, ivBuffer);


                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        byte[] valueBuffer = Encoding.UTF8.GetBytes(value);
                        cs.Write(valueBuffer, 0, valueBuffer.Length);
                        cs.FlushFinalBlock();

                        ms.Position = 0;
                        byte[] result = new byte[ms.Length];

                        ms.Read(result, 0, result.Length);

                        return Convert.ToBase64String(result);

                    }
                }
            }
            catch (Exception)
            {

                return "";
            }


        }

        public static string Encrypt(this string value)
        {
            return Encrypt(value, securityKey.ToMd5());
        }

        public static string Decrypt(this string value, string key)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "";

            value = value.Replace(" ", "+");

            try
            {
                using (var provider = new TripleDESCryptoServiceProvider())
                using (var md5 = new MD5CryptoServiceProvider())
                {
                    var ivBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"@{key}$"));
                    var keyBuffer = md5.ComputeHash(Encoding.UTF8.GetBytes($"#{key}!"));

                    var transform = provider.CreateDecryptor(keyBuffer, ivBuffer);


                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        byte[] valueBuffer = Convert.FromBase64String(value);
                        cs.Write(valueBuffer, 0, valueBuffer.Length);
                        cs.FlushFinalBlock();


                        ms.Position = 0;
                        byte[] result = new byte[ms.Length];

                        ms.Read(result, 0, result.Length);

                        return Encoding.UTF8.GetString(result);

                    }
                }
            }
            catch (Exception)
            {

                return "";
            }


        }

        public static string Decrypt(this string value)
        {

            return Decrypt(value, securityKey.ToMd5());
        }
    }
}
