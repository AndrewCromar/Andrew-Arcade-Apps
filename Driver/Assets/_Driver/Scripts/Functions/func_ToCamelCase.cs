using System.Collections.Generic;

namespace ONYX
{
    public class func_ToCamelCase
    {
        public string ToCamelCase(string _text)
        {
            string lower = _text.ToLower();
            List<string> wordList = new List<string>(lower.Split(' '));
            string firstWord = wordList[0];
            wordList.RemoveAt(0);
            string finalCamelcase = firstWord;
            foreach (string word in wordList)
            {
                finalCamelcase += char.ToUpper(word[0]) + word.Substring(1);
            }
            return finalCamelcase;
        }
    }
}
