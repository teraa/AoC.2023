int sum = 0;

string[] numbers = [
    "zero", "0",
    "one", "1",
    "two", "2",
    "three", "3",
    "four", "4",
    "five", "5",
    "six", "6",
    "seven", "7",
    "eight", "8",
    "nine", "9"
];

foreach (string line in GetInput())
{
    var span = line.AsSpan();
    int first = numbers.Select(selector: (x, i) => (Index: line.IndexOf(x), Value: i / 2))
        .OrderBy(x => x.Index)
        .First(x => x.Index >= 0)
        .Value;

    int last = numbers.Select(selector: (x, i) => (Index: line.LastIndexOf(x), Value: i / 2))
        .OrderBy(x => x.Index)
        .Last(x => x.Index >= 0)
        .Value;

    sum += first * 10 + last;
}

WriteLine(sum);
