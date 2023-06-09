using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaesarDecryption
{
    public class Decrypt
    {
        public string DecryptMessage(string message, int rotationKey)
        {
            string[] words = message.Split("_");
            List<string> decryptedWords = new List<string>();

            for (int i = 0; i < words.Length - 1; i += 2)
            {
                // Remove spaces from the encrypted word
                string encryptedWord = words[i].Replace(" ", "");

                //look at following number, if number > word.length, trim off last character
                int followingNumberWordLength = int.Parse(words[i + 1]);
                string correctedWord = encryptedWord.Substring(0, Math.Min(encryptedWord.Length, followingNumberWordLength));

                //Reverses the KEY shift value to each char in word
                decryptedWords.Add(CaesarDecrypt(correctedWord, rotationKey));
            }

            return string.Join(" ", decryptedWords).Trim();
        }

        private string CaesarDecrypt(string message, int rotation)
        {
            //I learned about the Caesar Cipher! I'd never heard of it before this
            var decryptedChars = new List<char>();
            foreach (char c in message)
            {
                int rotatedChar = c - rotation;
                if (c >= 65 && c <= 90)//if uppercase
                {
                    if (rotatedChar >= 65)
                        decryptedChars.Add((char)rotatedChar);
                    else//if rotatedChar is out of bounds, go to end of uppercase letters
                        decryptedChars.Add((char)(91 - (65 - rotatedChar)));
                }
                else if (c >= 97 && c <= 122)//if lowercase
                {
                    if (rotatedChar >= 97)
                        decryptedChars.Add((char)rotatedChar);
                    else //if rotatedChar is out of bounds, go to end of lowercase letters
                        decryptedChars.Add((char)(123 - (97 - rotatedChar)));
                }
                else
                {//if not a letter, just add to list
                    decryptedChars.Add(c);
                }
            }

            return string.Join("", decryptedChars);
        }

        public int GetShiftValue(string message)
        {
            Dictionary<char, int> frequencyAnalysis = CalculateFrequencyAnalysis(message.ToUpper());
            char mostFrequentLetter = MostFrequentLetter(frequencyAnalysis);
            int shiftValue = CalculateShiftValue(mostFrequentLetter, 'E');
            return shiftValue;
        }

        private Dictionary<char, int> CalculateFrequencyAnalysis(string text)
        {
            Dictionary<char, int> frequencyAnalysis = new Dictionary<char, int>();

            foreach (char c in text)
            {//counts occurance for each letter
                if (char.IsLetter(c))
                {
                    if (frequencyAnalysis.ContainsKey(c))
                        frequencyAnalysis[c]++;
                    else
                        frequencyAnalysis[c] = 1;
                }
            }

            return frequencyAnalysis;
        }

        private char MostFrequentLetter(Dictionary<char, int> frequencyAnalysis)
        {// returns most frequent by the highest Key count
            KeyValuePair<char, int> mostFrequent = frequencyAnalysis.OrderByDescending(x => x.Value).FirstOrDefault();
            return mostFrequent.Key;
        }

        private int CalculateShiftValue(char encryptedLetter, char decryptedLetter)
        {
            int encryptedAscii = (int)encryptedLetter;
            int decryptedAscii = (int)decryptedLetter;

            // Calculate the shift value
            int shiftValue = encryptedAscii - decryptedAscii;

            return shiftValue;
        }
    }
}
