using System.Collections;

namespace Advent.Year2023.Day07;

public sealed class Answer : IPuzzle<int>
{
    readonly struct Card(char value) : IComparable<Card>
    {
        public readonly char Value = value;

        public int Strength => Value switch
        {
            '2' => 0,
            '3' => 1,
            '4' => 2,
            '5' => 3,
            '6' => 4,
            '7' => 5,
            '8' => 6,
            '9' => 7,
            'T' => 8,
            'J' => 9,
            'Q' => 10,
            'K' => 11,
            'A' => 12,
            _ => -1, // Should never happen, but removes a warning.
        };

        public int CompareTo(Card other) => Strength.CompareTo(other.Strength);

        public override string ToString() => this.Value.ToString();
    }

    readonly struct Hand(ReadOnlySpan<char> raw) : IComparable<Hand>, IEnumerable<Card>
    {
        public readonly Card
            Card0 = new(raw[0]),
            Card1 = new(raw[1]),
            Card2 = new(raw[2]),
            Card3 = new(raw[3]),
            Card4 = new(raw[4]);

        public enum HandType
        {
            HighCard,
            OnePair,
            TwoPair,
            ThreeOfAKind,
            FullHouse,
            FourOfAKind,
            FiveOfAKind,
        }

        public HandType Type
        {
            get
            {
                var counts = this
                    .GroupBy(card => card)
                    .Select(group => group.Count())
                    .OrderByDescending(count => count)
                    .ToArray();

                if (counts.Length == 1)
                    return HandType.FiveOfAKind;

                if (counts[0] == 4)
                    return HandType.FourOfAKind;

                if (counts[0] == 3)
                {
                    if (counts[1] == 2)
                        return HandType.FullHouse;

                    return HandType.ThreeOfAKind;
                }

                if (counts[0] == 2)
                {
                    if (counts[1] == 2)
                        return HandType.TwoPair;

                    return HandType.OnePair;
                }

                return HandType.HighCard;
            }
        }

        public int CompareTo(Hand other)
        {
            int comparison;

            if ((comparison = Type.CompareTo(other.Type)) != 0)
                return comparison;

            if ((comparison = Card0.CompareTo(other.Card0)) != 0)
                return comparison;

            if ((comparison = Card1.CompareTo(other.Card1)) != 0)
                return comparison;

            if ((comparison = Card2.CompareTo(other.Card2)) != 0)
                return comparison;

            if ((comparison = Card3.CompareTo(other.Card3)) != 0)
                return comparison;

            return Card4.CompareTo(other.Card4);
        }

        public IEnumerator<Card> GetEnumerator()
        {
            yield return Card0;
            yield return Card1;
            yield return Card2;
            yield return Card3;
            yield return Card4;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() => string.Concat(this.Select(card => card.Value));
    }

    public int Part1(IEnumerable<string> input) => input
        .Select(line =>
        {
            var hand = new Hand(line.AsSpan(0, 5));
            var bid = int.Parse(line.AsSpan(6));

            return (Hand: hand, Bid: bid);
        })
        .OrderBy(hand => hand)
        .Select((hand, index) => hand.Bid * (index + 1))
        .Sum();

    public int Part2(IEnumerable<string> input)
    {
        return 0;
    }
}
