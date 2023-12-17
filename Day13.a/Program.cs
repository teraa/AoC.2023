var maps = GetInput()
    .ChunkBy("");

// maps = new List<List<string>>(){Enumerable.Repeat("", 7).ToList()};

int result = 0;

foreach (var map in maps)
{
    var length = (x: map[0].Length, y: map.Count);

    int j = 0, k = 0;

    int reflection = -1;

    for (int i = 0; i < length.y - 1; i++)
    {
        bool mismatch = false;

        Write($"{i} ");
        for (j = i + 1, k = i; j < length.y && k >= 0; j++, k--)
        {
            Write($"{j}-{k} ");
            if (map[j] != map[k])
            {
                mismatch = true;
                break;
            }
        }
        WriteLine();

        if (!mismatch)
        {
            reflection = i;
            break;
        }
    }

    if (reflection >= 0)
    {
        result += (reflection + 1)*100;
        WriteLine($"found y: {reflection}");
        continue;
    }

    for (int i = 0; i < length.x - 1; i++)
    {
        bool mismatch = false;

        Write($"{i} ");
        for (j = i + 1, k = i; j < length.x && k >= 0; j++, k--)
        {
            Write($"{j}-{k} ");

            if (!map.Select(line => line[j]).SequenceEqual(map.Select(line => line[k])))
            {
                mismatch = true;
                break;
            }
        }
        WriteLine();

        if (!mismatch)
        {
            reflection = i;
            break;
        }
    }

    if (reflection >= 0)
    {
        result += reflection + 1;
        WriteLine($"found x: {reflection}");
    }
}

WriteLine(result);
