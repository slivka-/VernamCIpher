using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace VernamCipher
{
    class Program
    {
        static void Main(string[] args)
        {
            int precision = 200;
            if (args.Length > 0)
                if (!int.TryParse(args[0], out precision))
                    precision = 200;

            Console.WriteLine("Input message:");
            string message = Console.ReadLine();

            Stopwatch s = new Stopwatch();
            s.Start();

            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] keyBytes = KeyGenerator.GenerateKey(messageBytes.Length, precision);
            byte[] outputBytes = new byte[messageBytes.Length];

            for (int i = 0; i < messageBytes.Length; i++)
                outputBytes[i] = (byte)(messageBytes[i] ^ keyBytes[i]);
            string encryptedMessage = Encoding.UTF8.GetString(outputBytes);

            Console.WriteLine("\nENCRYPTED MESSAGE:\n {0}",encryptedMessage);

            for (int i = 0; i < messageBytes.Length; i++)
                messageBytes[i] = (byte)(outputBytes[i] ^ keyBytes[i]);
            message = Encoding.UTF8.GetString(messageBytes);

            Console.WriteLine("\nDECRYPTED MESSAGE:\n {0}", message);

            s.Stop();
            Console.WriteLine("\nDone in {0}ms", s.ElapsedMilliseconds);

            Console.ReadLine();
        }
    }
}
