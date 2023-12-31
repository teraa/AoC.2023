﻿using System.Diagnostics;

// ReSharper disable MemberCanBePrivate.Global

namespace Common;

public static class Extensions
{
    private const string s_defaultFile = "input.txt";

    [Conditional("DEBUG")]
    public static void SetDebugInput(string filePath = s_defaultFile)
    {
        if (Console.IsInputRedirected)
            return;

        var file = File.OpenText(filePath);
        Console.SetIn(file);
    }

    public static IEnumerable<string> GetInput(string debugFilePath = s_defaultFile)
    {
        SetDebugInput(debugFilePath);
        while (Console.ReadLine() is { } line)
            yield return line;
    }

    public static IEnumerable<IReadOnlyList<T>> ChunkBy<T>(this IEnumerable<T> source, T separator)
        where T : IEquatable<T>
        => ChunkBy(source, separator.Equals);

    public static IEnumerable<IReadOnlyList<T>> ChunkBy<T>(this IEnumerable<T> source, Func<T, bool> predicate,
        bool keepSeparator = false)
    {
        List<T>? chunk = null;
        IReadOnlyList<T> empty = Array.Empty<T>();

        foreach (var element in source)
        {
            if (predicate(element))
            {
                if (keepSeparator)
                {
                    (chunk ??= new()).Add(element);
                }

                yield return chunk ?? empty;
                chunk = null;
            }
            else
            {
                (chunk ??= new()).Add(element);
            }
        }

        yield return chunk ?? empty;
    }

    public static IEnumerable<(int I, int J, T Item)> Flatten<T>(this IEnumerable<IEnumerable<T>> source)
        => source.SelectMany((line, i) => line.Select((e, j) => (i, j, e)));

    public static Dictionary<(int I, int J), T> Map<T>(this IEnumerable<(int i, int j, T e)> source)
        => source.ToDictionary(x => (x.i, x.j), x => x.e);

    public static Dictionary<(int I, int J), T> FlatMap<T>(this IEnumerable<IEnumerable<T>> source)
        => source.Flatten().Map();
}
