var map = GetInput()
    .SelectMany((row, i) =>
        row.Select((e, j) => (e, i, j)))
    .ToDictionary(x => (x.i, x.j), x => x.e);

int height = map.Keys.Max(x => x.i);
int length = map.Keys.Max(x => x.j);

for (int i = 1; i <= height; i++)
{
    for (int j = 0; j <= length; j++)
    {
        if (map[(i, j)] != 'O')
            continue;

        int k = i;
        while (k > 0 && map[(k - 1, j)] == '.')
        {
            k--;
        }

        if (k != i)
        {
            map[(k, j)] = 'O';
            map[(i, j)] = '.';
        }
    }
}

for (int i = 0; i <= height; i++)
{
    for (int j = 0; j <= length; j++)
    {
        Write(map[(i, j)]);
    }
    WriteLine();
}

int result = map.Where((pair) => pair.Value == 'O')
    .Select(pair => height - pair.Key.i + 1)
    .Sum();

WriteLine(result);

return;
