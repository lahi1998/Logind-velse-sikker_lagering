using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sikker_lagring__af__adgangskoder
{
    internal class SaltGenerator
    {
        public byte[] GenerateRandomNumber(int length)
        {
            var randomNumberGenerator = RandomNumberGenerator.Create();
            byte[] randomBytes = new byte[length];
            randomNumberGenerator.GetBytes(randomBytes);

            return randomBytes;
        }
    }
}