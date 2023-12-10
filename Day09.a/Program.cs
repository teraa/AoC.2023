var input = GetInput()
    .Select(line => line.Split()
        .Select(int.Parse)
        .ToList());

int result = 0;

foreach (var line in input)
{
    var sequences = new List<List<int>> {line};

    while (sequences[^1].Any(x => x is not 0))
    {
        var prev = sequences[^1];
        var seq = new List<int>();
        sequences.Add(seq);
        for (int i = 1; i < prev.Count; i++)
        {
            seq.Add(prev[i] - prev[i - 1]);
        }
    }

    for (int i = sequences.Count - 1; i > 0; i--)
    {
        var seq = sequences[i];
        var prev = sequences[i - 1];
        prev.Add(prev[^1] + seq[^1]);
    }

    result += sequences[0][^1];
}

WriteLine(result);
