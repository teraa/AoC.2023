using System.Diagnostics;

var hands = GetInput()
    .Select(x => x.Split())
    .Select(parts => new Hand(parts[0], int.Parse(parts[1])));

int result = hands
    .OrderDescending()
    .Select((hand, i) => hand.Bid * (i + 1))
    .Sum();

WriteLine(result);


sealed class Hand : IComparable<Hand>
{
    public Hand(string cards, int bid)
    {
        Cards = cards;
        Bid = bid;

        var groups = Cards.GroupBy(x => x)
            .Select(x => x.Count())
            .OrderDescending()
            .ToList();

        Name = groups switch
        {
            [5] => Name.FiveKind,
            [4, 1] => Name.FourKind,
            [3, 2] => Name.FullHouse,
            [3, 1, 1] => Name.ThreeKind,
            [2, 2, 1] => Name.TwoPair,
            [2, 1, 1, 1] => Name.OnePair,
            [1, 1, 1, 1, 1] => Name.HighCard,
            _ => throw new UnreachableException()
        };
    }

    public string Cards { get; }
    public int Bid { get; }
    public Name Name { get; }

    public int CompareTo(Hand? other)
    {
        Debug.Assert(other is not null);

        const string labels = "AKQJT98765432";

        int result = Name.CompareTo(other.Name);
        if (result != 0)
            return result;

        foreach (var (a, b) in Cards.Zip(other.Cards))
        {
            result = labels.IndexOf(a) - labels.IndexOf(b);
            if (result != 0)
                return Math.Sign(result);
        }

        return 0;
    }
};

enum Name
{
    FiveKind,
    FourKind,
    FullHouse,
    ThreeKind,
    TwoPair,
    OnePair,
    HighCard
}
