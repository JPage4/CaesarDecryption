using System;
using System.Text;
using System.Collections.Generic;

namespace CaesarDecryption
{
    public class Program
    {
        const string SECRET_MESSAGE = "Ez ol jd _5_ jz f7 _3_ sl gp _4_ nc ln vp o9 _7_ l6 _1_ df ap cQ _5_ dp nc pe _6_ nz op _4_ es le _4_ hl dz _3_ py nc ja ep oH _9_ fd ty ru _5_ ly _2_ lw rz ct es xC _9_ op gt dp oy _7_ mj _2_ l9 _1_ ep lx _4_ zq _2_ dp nf ct ej _8_ da pn tl wt de dZ _11_ ez _2_ mp _2_ tx az dd tm wp _10_ ez _2_ nc ln v. _6_ Gp cj _4_ hp ww _4_ oz yp !M _5_";

        public static void Main()
        {
            // THIS IS JUST AN EXAMPLE FOR ILLUSTRATIVE PURPOSES. 
            // IT IS NOT RELATED TO THE SECRET MESSAGE.

            string exampleMessage = "Just a test.";
            int exampleKey = 4;

            string exampleResult = EncryptMessage(exampleMessage, exampleKey);
            Console.WriteLine("Encrypted example: " + exampleResult);

            Decrypt decrypt = new Decrypt();
            //Originally I ran a for loop 26 to find what the shift key should be
            //a better way to do this is to search SECRET_MESSAGE for the letter that occurs most frequently and
            //calculate the shift based off of "E" which is the letter that statistically occurs most often

            int shiftValue = decrypt.GetShiftValue(SECRET_MESSAGE);
            string decryptResult = decrypt.DecryptMessage(SECRET_MESSAGE, shiftValue);
            Console.WriteLine("Decrypted example: " + decryptResult);

        }

        //Below is starting code given by Applied Systems to solve decryption code challenge
        //SECRET MESSAGE WAS CREATED USING THIS METHOD.
        public static string EncryptMessage(string message, int key)
        {
            var encryptedMessage = CaesarEncrypt(message, key);
            return FormatMessage(encryptedMessage, 2);
        }

        private static string CaesarEncrypt(string message, int rotation)
        {
            // ASCII letter ranges
            // A - 65, Z - 90
            // a - 97, z - 122

            var rotatedChars = new List<char>();

            foreach (char c in message)
            {
                int rotatedChar = c + rotation;
                if (c >= 65 && c <= 90)
                {
                    if (rotatedChar <= 90)
                        rotatedChars.Add((char)rotatedChar);
                    else
                        rotatedChars.Add((char)(64 + (rotatedChar - 90)));
                }
                else if (c >= 97 && c <= 122)
                {
                    if (rotatedChar <= 122)
                        rotatedChars.Add((char)rotatedChar);
                    else
                        rotatedChars.Add((char)(96 + (rotatedChar - 122)));
                }
                else
                {
                    rotatedChars.Add(c);
                }
            }

            return string.Join("", rotatedChars);
        }

        private static string FormatMessage(string message, int padding)
        {
            var words = message.Split();
            var preparedWords = new List<string>();
            foreach (var word in words)
            {
                preparedWords.Add(SplitAndPadWord(word, padding));
            }
            return string.Join(" ", preparedWords).Trim();
        }

        private static string SplitAndPadWord(string word, int padding)
        {
            int numberOfParts = (int)Math.Ceiling((double)word.Length / padding);

            string result;
            if (numberOfParts > 1)
            {
                List<string> wordParts = new List<string>();

                for (int part = 0; part < numberOfParts; part++)
                {
                    if (part < (numberOfParts - 1))
                        wordParts.Add(word.Substring(part * padding, padding));
                    else
                    {
                        var unpaddedPart = word.Substring(part * padding);
                        wordParts.Add(PadPart(unpaddedPart, padding));
                    }
                }
                result = string.Join(" ", wordParts.ToArray()).Trim();
            }
            else
                result = PadPart(word, padding);

            return string.Format("{0} _{1}_", result, word.Length);
        }

        private static string PadPart(string part, int padding)
        {
            var sb = new StringBuilder(part);
            var paddingNeeded = padding - part.Length;

            for (int iteration = 0; iteration < paddingNeeded; iteration++)
                sb.Append(GetRandomChar());

            return sb.ToString();
        }

        private static Random rand = new Random();
        private static char GetRandomChar()
        {
            var possibleChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return possibleChars[rand.Next(0, possibleChars.Length)];
        }
    }
}