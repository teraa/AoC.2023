var chunks = GetInput()
    .ChunkBy("")
    .ToList();

var seeds = chunks[0][0]
    .Split(" ")
    .Skip(1)
    .Select(long.Parse)
    .ToList();

var maps = chunks.Skip(1)
    .Select(chunk => chunk.Skip(1)
        .Select(line => line.Split(" ")
            .Select(long.Parse)
            .ToList())
        .Select(x => (Destination: x[0], Source: x[1], Length: x[2]))
        .ToList())
    .ToList();

foreach (var map in maps)
{
    for (int i = 0; i < seeds.Count; i++)
    {
        var translation = map.FirstOrDefault(x => x.Source <= seeds[i] && x.Source + x.Length >= seeds[i]);

        seeds[i] = seeds[i] + translation.Destination - translation.Source;
    }
}

WriteLine(seeds.Min());
