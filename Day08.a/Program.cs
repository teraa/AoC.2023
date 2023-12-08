using System.Text.RegularExpressions;

const string start = "AAA";
const string end = "ZZZ";

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

int result = 0;

string position = start;
do
{
    int move = steps[result % steps.Length] == 'L' ? 0 : 1;
    position = map[position][move];
    result++;
} while (position != end);

WriteLine(result);
