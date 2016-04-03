using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Cryptogram
{
    public static class Base64
    {
        public static string Encrypt(string value)
        {
            string base64string = String.Empty;
            byte[] bytes = Encoding.UTF8.GetBytes(value);
            try
            {
                base64string = Convert.ToBase64String(bytes);
            }
            catch
            {
                base64string = value;
            }
            return base64string;
        }
    }
}
