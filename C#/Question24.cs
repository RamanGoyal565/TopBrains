using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public record Student(string Name, int Score);

public class StudentProcessor
{
    public static string BuildAndSerialize(string[] items, int minScore)
    {
        if (items == null || items.Length == 0)
            return "[]";

        var students = new List<Student>(items.Length);

        foreach (var item in items)
        {
            var parts = item.Split(':');
            if (parts.Length != 2)
                continue;

            if (int.TryParse(parts[1], out int
