int sum = 0;

foreach (string line in GetInput())
{
    string[] parts = line.Split(": ");
    int game = int.Parse(parts[0].AsSpan(5..));
    string[] sets = parts[1].Split("; ");

    bool isOverAllowance = false;
    foreach (string set in sets)
    {
        string[] cubes = set.Split(", ");
        foreach (ReadOnlySpan<char> cube in cubes)
        {
            int i = cube.IndexOf(' ');
            int shown = int.Parse(cube[..i]);
            int allowed = cube[(i + 1)..] switch
            {
                "red" => 12,
                "green" => 13,
                _ => 14,
            };

            if (shown > allowed)
            {
                isOverAllowance = true;
                break;
            }
        }

        if (isOverAllowance)
            break;
    }

    if (!isOverAllowance)
        sum += game;
}

WriteLine(sum);
