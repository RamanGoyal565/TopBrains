using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class TopScorer
    {
        public static void main()
        {
            List<string> names = new List<string>() 
            { "Ravi,87",
            "Kumar,98",
            "Arun,92",
            "Deepak,80",
            "Kiran,95"};

            List<string> topScorers = names.Select(s => s.Split(","))
            .Select(p => new { Name = p[0], Marks = int.Parse(p[1]) })
            .OrderByDescending(s=>s.Marks).Take(3)
            .Select(s=>s.Name).ToList();

            foreach (var name in topScorers)
            {
                Console.WriteLine(name);
            }
        }

    }
}
