int result = GetInput()
    .First()
    .Split(",")
    .Sum(step => step.Aggregate(0, (int acc, char src) => (acc + src) * 17 % 256));

WriteLine(result);
