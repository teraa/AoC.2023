var image = GetInput()
    .Select((line, i) =>
        line.Select((e, j) => (i, j, e))
            .Where(e => e.e is '#')
            .Select(e => (e.i, e.j))
            .ToList())
    .SelectMany(x => x)
    .ToList();

var v = image.Select(x => x.i)
    .Distinct()
    .ToList();

var h = image.Select(x => x.j)
    .Distinct()
    .ToList();

int size = v.Concat(h).Max();
var range = Enumerable.Range(0, size);

var addV = range.Except(v).ToList();
var addH = range.Except(h).ToList();

var newImage = image.Select(e => (
        i: (e.i + addV.Count(x => x < e.i)),
        j: (e.j + addH.Count(x => x < e.j)))
    ).ToList();

int result = 0;

for (int i = 0; i < newImage.Count; i++)
{
    for (int j = i + 1; j < newImage.Count; j++)
    {
        int d = Math.Abs(newImage[j].i - newImage[i].i)
            + Math.Abs(newImage[j].j - newImage[i].j);

        result += d;
    }
}

WriteLine(result);
