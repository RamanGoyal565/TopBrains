using System;
using System.Collections.Generic;
using System.Text;

public class FindDuplicate
{
    public static void main()
    {
        List<string> list = new List<string>() { "Pen", "Book", "Pen", "Pencil", "Book" };

        List<string> duplicateList = list.GroupBy(x => x).Where(x => x.Count() > 1).Select(x => x.Key).ToList();
        foreach (var item in duplicateList)
        {
            Console.WriteLine(item);
        }
    }
}

