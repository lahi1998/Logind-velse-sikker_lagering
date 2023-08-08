using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Policy;

namespace Sikker_lagring__af__adgangskoder
{
    internal class Hasher
    {
        public byte[] Hash(byte[] Bytes)
        {
            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashvalue = sha512.ComputeHash(Bytes);

                return hashvalue;
            }
        }
    }
}
