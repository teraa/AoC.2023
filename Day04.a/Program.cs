int sum = 0;

foreach (ReadOnlySpan<char> line in GetInput())
{
    int i = line.IndexOf(':');
    int j = line.IndexOf('|');

    var winning = line[(i + 1)..(j - 1)];
    var selected = line[(j + 1)..];
    int matches = 0;

    for (int k = 0; k < winning.Length; k += 3)
    {
        if (selected.Contains(winning.Slice(k, 3), StringComparison.Ordinal))
            matches++;
    }

    if (matches > 0)
        sum += 1 << (matches - 1);
}

WriteLine(sum);
