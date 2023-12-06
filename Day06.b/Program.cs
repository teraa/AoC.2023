using System.Text.RegularExpressions;

var regex = new Regex(@"[^\d]+", RegexOptions.Compiled);

var input = GetInput()
    .Select(line => regex.Replace(line, ""))
    .Select(long.Parse)
    .ToList();

var time = input[0];
var record = input[1];

int result = 0;

for (int i = 1; i <= time; i++)
{
    if ((time - i) * i > record)
        result++;
}

WriteLine(result);
