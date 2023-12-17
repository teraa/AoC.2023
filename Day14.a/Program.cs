var map = GetInput()
    .Select(x => x.ToArray())
    .ToArray();

int height = map.Length;
int length = map[0].Length;

for (int i = 1; i < height; i++)
{
    for (int j = 0; j < length; j++)
    {
        if (map[i][j] != 'O')
            continue;

        int k = i;
        while (k > 0 && map[k - 1][j] == '.')
        {
            k--;
        }

        if (k != i)
        {
            map[k][j] = 'O';
            map[i][j] = '.';
        }
    }
}

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < length; j++)
    {
        Write(map[i][j]);
    }
    WriteLine();
}

int result = map.Flatten()
    .Where(x => x.Item == 'O')
    .Sum(x => height - x.I);

WriteLine(result);
