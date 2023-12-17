using System.Diagnostics;

var values = GetInput()
    .Select(line => line.Select(GetPipe)
        .ToList())
    .ToList();

var points = new List<Point>();

Point start = values.Select((row, i) => (i, j: row.IndexOf(Direction.All)))
    .Where(x => x.j >= 0)
    .Select(x => new Point(x.j, x.i))
    .First();

int sizeY = values.Count;
int sizeX = values[0].Count;

points.Add(start);

var current = start;

do
{
    Point? next = null;

    var value = values[current.Y][current.X];

    for (int y = -1; y <= 1; y++)
    {
        for (int x = -1; x <= 1; x++)
        {
            if (x == y || x == -y)
                continue;

            var offset = new Point(x, y);

            var k = offset + current;

            if (points.Count > 1 && points[^2] == k)
                continue;

            if (!IsInBounds(k))
                continue;

            var offsetDirection = OffsetToDirection(offset);
            if (!value.HasFlag(offsetDirection))
                continue;

            var connectingDirection = GetConnectingDirection(offsetDirection);
            var newValue = values[k.Y][k.X];

            if (!newValue.HasFlag(connectingDirection))
                continue;

            next = k;
            break;
        }

        if (next is not null)
            break;
    }

    Debug.Assert(next is not null);

    points.Add(next.Value);
    current = next.Value;
} while (current != start);

points.RemoveAt(points.Count - 1);

// TODO: depends on input
values[start.Y][start.X] = Direction.Vertical; // original
// values[start.Y][start.X] = Direction.DownRight; // example

int result = 0;

for (int y = 0; y < sizeY; y++)
{
    for (int x = 0; x < sizeX; x++)
    {
        var point = new Point(x, y);
        if (points.Contains(point))
            continue;

        if (IsInside(point))
            result++;
    }
}

WriteLine(result);

bool IsInside(Point p)
{
    int upCuts = 0;

    for (int i = p.X; i < sizeX; i++)
    {
        Point n = p with {X = i};
        if (!points.Contains(n))
            continue;

        if (values[n.Y][n.X].HasFlag(Direction.Up))
            upCuts++;
    }

    return upCuts % 2 == 1;
}

static Direction GetConnectingDirection(Direction direction) => direction switch
{
    Direction.Up => Direction.Down,
    Direction.Down => Direction.Up,
    Direction.Right => Direction.Left,
    Direction.Left => Direction.Right,
    _ => throw new UnreachableException(),
};

static Direction OffsetToDirection(Point offset) => offset switch
{
    (-1, 0) => Direction.Left,
    (1, 0) => Direction.Right,
    (0, -1) => Direction.Up,
    (0, 1) => Direction.Down,
    _ => throw new UnreachableException(),
};

bool IsInBounds(Point point)
    => point.X >= 0 && point.X < sizeX && point.Y >= 0 && point.Y < sizeY;

static Direction GetPipe(char c) => c switch
{
    '|' => Direction.Vertical,
    '-' => Direction.Horizontal,
    'L' => Direction.UpRight,
    'J' => Direction.UpLeft,
    '7' => Direction.DownLeft,
    'F' => Direction.DownRight,
    '.' => Direction.None,
    'S' => Direction.All,
    _ => throw new UnreachableException(),
};

[Flags]
enum Direction
{
    None = 0,
    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,
    All = ~(-1 << 4),

    Vertical = Up | Down,
    Horizontal = Left | Right,
    UpLeft = Up | Left,
    UpRight = Up | Right,
    DownLeft = Down | Left,
    DownRight = Down | Right,
}

record struct Point(int X, int Y)
{
    public static Point operator +(Point a, Point b)
        => new(a.X + b.X, a.Y + b.Y);

    public static Point operator -(Point a, Point b)
        => new(a.X - b.X, a.Y - b.Y);
}
