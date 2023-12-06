using System.Text.RegularExpressions;

var regex = new Regex(@"\d+");

var input = GetInput()
    .Select(line => regex.Matches(line)
        .Select(match => int.Parse(match.Value)))
    .ToList();

int result = 1;

foreach (var (time, record) in input[0].Zip(input[1]))
{
    result *= Enumerable.Range(1, time - 1)
        .Count(i => (time - i) * i > record);
}

WriteLine(result);
