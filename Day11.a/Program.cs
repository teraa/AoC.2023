var image = GetInput()
    .Flatten()
    .Where(x => x.Item is '#')
    .Select(x => (x.I, x.J))
    .ToList();

var v = image.Select(x => x.I)
    .Distinct()
    .ToList();

var h = image.Select(x => x.J)
    .Distinct()
    .ToList();

int size = v.Concat(h).Max();
var range = Enumerable.Range(0, size).ToList();

var addV = range.Except(v).ToList();
var addH = range.Except(h).ToList();

var newImage = image.Select(e => (
    i: (e.I + addV.Count(x => x < e.I)),
    j: (e.J + addH.Count(x => x < e.J)))
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
