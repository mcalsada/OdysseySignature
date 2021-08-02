using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace OdysseySignature
{
    class Program
    {
        static void Main(string[] args)
        {            
            if (args.Length != 1)
            {
                Console.WriteLine($"Error: Invalid arguments");
                Console.WriteLine($"Expected Usage: OdysseySignature.exe <FileName>");
                return;
            }
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"Error: {args[0]} is not a valid file");
                return;
            }
            
            var fileInfo = new FileInfo(args[0]);

            SHA1CryptoServiceProvider sha1Encryptor = new SHA1CryptoServiceProvider();

            FileStream stream = null;

            try
            {
                stream = fileInfo.OpenRead();

                sha1Encryptor.ComputeHash(stream);
            }
            finally
            {
                stream.Close();
            }

            byte[] hash = sha1Encryptor.Hash;

            Console.WriteLine($"Signature: {{SHA1}}{Convert.ToBase64String(hash)}");
        }
    }
}
