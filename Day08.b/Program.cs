using System.Text.RegularExpressions;

var chunks = GetInput()
    .ChunkBy("")
    .ToList();

string steps = chunks[0][0];
var regex = new Regex(@"\w+", RegexOptions.Compiled);

var map = chunks[1]
    .Select(line => regex.Matches(line)
        .Select(match => match.Value)
        .ToList())
    .ToDictionary(x => x[0], x => x[1..]);

var positions = map.Keys.Where(x => x[^1] == 'A')
    .ToList();

var results = new long[positions.Count];

for (int i = 0; i < positions.Count; i++)
{
    string position = positions[i];

    int j = 0;
    do
    {
        int move = steps[j % steps.Length] == 'L' ? 0 : 1;
        position = map[position][move];
        j++;
    } while (position[^1] != 'Z');

    results[i] = j;
}

long lcm = results.Aggregate(Lcm);

WriteLine(lcm);

static long Lcm(long a, long b)
{
    return a * b / Gcd(a, b);
}

static long Gcd(long a, long b)
{
    if (a < b)
        (a, b) = (b, a);

    if (b == 0)
        return a;

    return Gcd(a % b, b);
}
