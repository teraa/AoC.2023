int sum = 0;

foreach (string line in GetInput())
{
    var span = line.AsSpan();
    int i = span.IndexOf(':');
    int game = int.Parse(span[5..i]);
    string[] sets = span[(i+2)..].ToString().Split("; ");

    var (r, g, b) = (0, 0, 0);

    foreach (string set in sets)
    {
        string[] cubes = set.Split(", ");
        foreach (string cube in cubes)
        {
            i = cube.IndexOf(' ');
            int shown = int.Parse(cube[..i]);
            string color = cube[(i + 1)..];

            ref int x = ref r;
            switch(color)
            {
                case "green": x = ref g; break;
                case "blue": x = ref b; break;
            }

            if (x < shown)
                x = shown;
        }
    }

    sum += r * g * b;
}

WriteLine(sum);
