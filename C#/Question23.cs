using System;
using System.Collections.Generic;

public static class DistinctByExtensions
{
    public static string[] DistinctNamesById(this string[] items)
    {
        if (items == null || items.Length == 0)
            return Array.Empty<string>();

        var seenIds = new HashSet<string>();
        var result = new List<string>();

        foreach (var item in items)
        {
            if (string.IsNullOrEmpty(item))
                continue;

            int index = item.IndexOf(':');
            if (index <= 0 || index == item.Length - 1)
                continue;

            string id = item.Substring(0, index);
            string name = item.Substring(index + 1);

            if (seenIds.Add(id))
            {
                result.Add(name);
            }
        }

        return result.ToArray();
    }
}
