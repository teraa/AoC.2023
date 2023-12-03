using System.Security.Cryptography.X509Certificates;

var numbers = new List<Number>();
var symbols = new HashSet<Point>();

int i = 0;
foreach (string line in GetInput())
{
    int start = -1;
    int n = 0;
    for (int j = 0; j < line.Length; j++)
    {
        char c = line[j];
        if (c is >= '0' and <= '9')
        {
            if (start == -1)
            {
                start = j;
                n = c - '0';
            }
            else
            {
                n = n * 10 + (c - '0');
            }

            if (j == line.Length - 1)
            {
                numbers.Add(new Number(new Point(i, start), n, j - start));
            }
        }
        else
        {
            if (start >= 0)
            {
                numbers.Add(new Number(new Point(i, start), n, j - start));
            }

            if (c is not '.')
            {
                symbols.Add(new Point(i, j));
            }

            start = -1;
            n = 0;
        }
    }
    i++;
}

int sum = 0;
foreach (var number in numbers)
{
    bool valid = false;

    for (i = -1; i <= 1 && !valid; i++)
    {
        for (int j = -1; j <= number.Length && !valid; j++)
        {
            if (i == 0 && j == 0)
                continue;

            var (x, y) = number.Point;
            x += i;
            y += j;

            if (symbols.Contains(new Point(x, y)))
            {
                valid = true;
            }
        }
    }

    if (valid)
        sum += number.Value;
}

WriteLine(sum);

record struct Point(int X, int Y);

record struct Number(Point Point, int Value, int Length);
