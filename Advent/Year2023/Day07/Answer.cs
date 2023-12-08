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

    readonly struct Hand : IComparable<Hand>
    {
        public readonly Card Card0, Card1, Card2, Card3, Card4;
        public readonly HandType Type;

        public Hand(ReadOnlySpan<char> raw)
        {
            Card0 = new(raw[0]);
            Card1 = new(raw[1]);
            Card2 = new(raw[2]);
            Card3 = new(raw[3]);
            Card4 = new(raw[4]);

            Type = ComputeType(Card0, Card1, Card2, Card3, Card4);
        }

        static HandType ComputeType(params Card[] cards)
        {
            var counts = cards
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

        public override string ToString() => string.Concat(Card0.Value, Card1.Value, Card2.Value, Card3.Value, Card4.Value);
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

    readonly struct CardWithJoker(char value) : IComparable<CardWithJoker>
    {
        public readonly char Value = value;

        public int Strength => Value switch
        {
            'J' => 0,
            '2' => 1,
            '3' => 2,
            '4' => 3,
            '5' => 4,
            '6' => 5,
            '7' => 6,
            '8' => 7,
            '9' => 8,
            'T' => 9,
            'Q' => 10,
            'K' => 11,
            'A' => 12,
            _ => -1, // Should never happen, but removes a warning.
        };

        public int CompareTo(CardWithJoker other) => Strength.CompareTo(other.Strength);

        public override string ToString() => this.Value.ToString();
    }

    readonly struct HandWithJoker : IComparable<HandWithJoker>
    {
        public readonly CardWithJoker Card0, Card1, Card2, Card3, Card4;
        public readonly HandType Type;

        public HandWithJoker(ReadOnlySpan<char> raw)
        {
            Card0 = new(raw[0]);
            Card1 = new(raw[1]);
            Card2 = new(raw[2]);
            Card3 = new(raw[3]);
            Card4 = new(raw[4]);

            Type = ComputeType(Card0, Card1, Card2, Card3, Card4);
        }

        static HandType ComputeType(params CardWithJoker[] cards)
        {
            var jokers = cards.Count(card => card.Value == 'J');
            var counts = cards
                .Where(card => card.Value != 'J')
                .GroupBy(card => card)
                .Select(group => group.Count())
                .OrderByDescending(count => count)
                .ToArray();

            if (counts.Length <= 1)
                return HandType.FiveOfAKind;

            counts[0] += jokers;

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

        public int CompareTo(HandWithJoker other)
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

        public override string ToString() => string.Concat(Card0.Value, Card1.Value, Card2.Value, Card3.Value, Card4.Value);
    }

    public int Part2(IEnumerable<string> input) => input
        .Select(line =>
        {
            var hand = new HandWithJoker(line.AsSpan(0, 5));
            var bid = int.Parse(line.AsSpan(6));

            return (Hand: hand, Bid: bid);
        })
        .OrderBy(hand => hand)
        .Select((hand, index) => hand.Bid * (index + 1))
        .Sum();
}
