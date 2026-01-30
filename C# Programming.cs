using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string word1 = Console.ReadLine();
        string word2 = Console.ReadLine();

        HashSet<char> vowels = new HashSet<char> { 'a', 'e', 'i', 'o', 'u' };
        HashSet<char> word2Chars = new HashSet<char>(word2.ToLower());

        List<char> filtered = new List<char>();
        foreach (char c in word1)
        {
            char lower = char.ToLower(c);
            if (vowels.Contains(lower) || !word2Chars.Contains(lower))
                filtered.Add(c);
        }

        List<char> result = new List<char>();
        foreach (char c in filtered)
        {
            if (result.Count == 0 || char.ToLower(result[result.Count - 1]) != char.ToLower(c))
                result.Add(c);
        }

        Console.WriteLine(new string(result.ToArray()));
    }
}
