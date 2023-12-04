var results = new List<int>();

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

    results.Add(matches);
}

var cards = Enumerable.Repeat(1, results.Count).ToList();

for (int i = 0; i < cards.Count; i++)
{
    for (int j = 1; j <= results[i]; j++)
    {
        cards[i + j] += cards[i];
    }
}

WriteLine(cards.Sum());
