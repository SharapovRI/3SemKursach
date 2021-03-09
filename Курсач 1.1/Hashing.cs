using System;
using System.Security.Cryptography;
using System.Text;

namespace Курсач_1._1
{
    public static class Hashing
    {
        public static string GetHash(string input)
        {
            var md5 = SHA512.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return BitConverter.ToString(hash).Replace("-", "");
        }
    }
}
