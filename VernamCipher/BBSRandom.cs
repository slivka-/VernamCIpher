using System.Numerics;
using System.Security.Cryptography;

namespace VernamCipher
{
    class BBSRandom
    {
        private BigInteger previousValue;
        private BigInteger n;

        public BBSRandom(int precision)
        {
            PrimeGenerator primeGenerator = new PrimeGenerator(precision);
            BigInteger p;
            BigInteger q;
            do
                p = primeGenerator.Get512BitPrime();
            while (p % 4 != 3);

            do
                q = primeGenerator.Get512BitPrime();
            while (q % 4 != 3);

            n = p * q;

            byte[] bytes = new byte[64];

            BigInteger s;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                do
                {
                    rng.GetBytes(bytes);
                    s = new BigInteger(bytes);
                }
                while (s < 1 || s > n-1);
            }

            previousValue = (s * s) % n;
        }

        public BigInteger Next()
        {
            previousValue = (previousValue * previousValue) % n;
            return previousValue;
        }
    }
}
