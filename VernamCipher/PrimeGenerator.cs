using System;
using System.Numerics;
using System.Security.Cryptography;

namespace VernamCipher
{
    class PrimeGenerator
    {
        private int iterNum;
        private Random random;

        public PrimeGenerator(int precision)
        {
            iterNum = precision;
            random = new Random();
        }

        public BigInteger Get512BitPrime()
        {
            byte[] bytes = new byte[64];
            BigInteger output;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                do
                {
                    rng.GetBytes(bytes);
                    output = new BigInteger(bytes);
                }
                while (!CheckPrime(output));
            }
            return output;
        }

        private bool CheckPrime(BigInteger number)
        {
            if (number < 2)
                return false;
            if (number > 0 && number % 2 == 0)
                return false;

            BigInteger s = number - 1;
            while (s % 2 == 0)
                s /= 2;

            for (int i = 0; i < iterNum; i++)
            {

                BigInteger a = (random.Next() % (number - 1)) + 1;
                BigInteger temp = s;
                BigInteger mod = ModExponent(a, temp, number);
                while (temp != number - 1 && mod != 1 && mod != number - 1)
                {
                    mod = (mod * mod) % number;
                    temp *= 2;
                }
                if (mod != number - 1 && temp % 2 == 0)
                    return false;
            }
            return true;
        }

        private BigInteger ModExponent(BigInteger modBase, BigInteger modExp, BigInteger mod)
        {
            BigInteger x = 1;
            BigInteger y = modBase;
            while (modExp > 0)
            {
                if (modExp % 2 == 1)
                    x = (x * y) % mod;
                y = (y * y) % mod;
                modExp = modExp / 2;
            }
            return x % mod;
        }
    }
}
