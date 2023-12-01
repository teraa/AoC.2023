using System.Buffers;

int sum = 0;
var searchValues = SearchValues.Create("0123456789");

foreach (string line in GetInput())
{
    var span = line.AsSpan();
    int i = span.IndexOfAny(searchValues);
    int j = span.LastIndexOfAny(searchValues);
    sum += (span[i] - '0') * 10 + span[j] - '0';
}

WriteLine(sum);
