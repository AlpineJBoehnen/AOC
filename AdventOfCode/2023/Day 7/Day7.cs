namespace AdventOfCode.Y2023;

public class Day7 : AdventOfCodeDay
{
    /*
     * Every hand is exactly one type. From strongest to weakest, they are:

Five of a kind, where all five cards have the same label: AAAAA
Four of a kind, where four cards have the same label and one card has a different label: AA8AA
Full house, where three cards have the same label, and the remaining two cards share a different label: 23332
Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98
Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432
One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4
High card, where all cards' labels are distinct: 23456
Hands are primarily ordered based on type; for example, every full house is stronger than any three of a kind.

If two hands have the same type, a second ordering rule takes effect. Start by comparing the first card in each hand. If these cards are different, the hand with the stronger first card is considered stronger. If the first card in each hand have the same label, however, then move on to considering the second card in each hand. If they differ, the hand with the higher second card wins; otherwise, continue with the third card in each hand, then the fourth, then the fifth.

So, 33332 and 2AAAA are both four of a kind hands, but 33332 is stronger because its first card is stronger. Similarly, 77888 and 77788 are both a full house, but 77888 is stronger because its third card is stronger (and both hands have the same first and second card).
     */

    public Day7() : base(2023, 7)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        SortedList<int, Part1Hand> hands = new();

        foreach (string line in input)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Part1Hand hand = new(parts[0], int.Parse(parts[1]));
            hands.Add(hand.GetKey(), hand);
        }

        ulong total = 0;
        for (int ii = 0; ii < hands.Count; ii++)
        {
            total += (ulong)hands.GetValueAtIndex(ii).Bid * (ulong)(ii + 1);
        }

        return total.ToString();
    }

    protected override string SolvePart2(string[] input)
    {
        SortedList<int, Part2Hand> hands = new();

        foreach (string line in input)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Part2Hand hand = new(parts[0], int.Parse(parts[1]));
            int key = hand.GetKey();
            if (hands.ContainsKey(key))
            {
                if (hand.IsHigherWithJokers(hands[key]))
                {
                    key++;
                }
                else
                {
                    key--;
                }
            }
            hands.Add(key, hand);
        }

        ulong total = 0;
        for (int ii = 0; ii < hands.Count; ii++)
        {
            total += (ulong)hands.GetValueAtIndex(ii).Bid * (ulong)(ii + 1);
        }

        return total.ToString();
    }

    private class Part1Hand
    {
        public static Dictionary<char, char> CardRanks = new()
        {
            {'2', '2' },
            { '3', '3' },
            { '4', '4' },
            { '5', '5' },
            { '6', '6' },
            { '7', '7' },
            { '8', '8' },
            { '9', '9' },
            { 'T', 'a' },
            { 'J', 'b' },
            { 'Q', 'c' },
            { 'K', 'd' },
            { 'A', 'e'}
        };

        // five chars - five cards
        public string Cards;
        public int Bid;

        public Part1Hand(string cards, int bid)
        {
            Cards = cards.ToUpper();
            Bid = bid;
        }

        public int GetKey()
        {
            char[] score = ['0', '0', '0', '0', '0', '0']; // type, first card, second card, third card, fourth card, fifth card

            if (IsFiveOfAKind())
            {
                score[0] = '7';
            }
            else if (IsFourOfAKind())
            {
                score[0] = '6';
            }
            else if (IsFullHouse())
            {
                score[0] = '5';
            }
            else if (IsThreeOfAKind())
            {
                score[0] = '4';
            }
            else if (IsTwoPair())
            {
                score[0] = '3';
            }
            else if (IsOnePair())
            {
                score[0] = '2';
            }
            else if (IsHighCard())
            {
                score[0] = '1';
            }

            for (int ii = 0; ii < Cards.Length; ii++)
            {
                score[ii + 1] = CardRanks[Cards[ii]];
            }

            string hex = string.Join("", score);
            return Convert.ToInt32(hex, 16);
        }

        public bool IsFiveOfAKind()
        {
            return Cards.Distinct().Count() == 1;
        }

        public bool IsFourOfAKind()
        {
            foreach (var rank in CardRanks.Keys)
            {
                if (Cards.Count(_ => _ == rank) == 4)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsFullHouse()
        {
            bool foundTwo = false;
            foreach (var rank in CardRanks.Keys)
            {
                if (Cards.Count(_ => _ == rank) == 2)
                {
                    foundTwo = true;
                }
            }

            if (!foundTwo)
            {
                return false;
            }

            foreach (var rank in CardRanks.Keys)
            {
                if (Cards.Count(_ => _ == rank) == 3)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsThreeOfAKind()
        {
            if (Cards.Distinct().Count() != 3)
            {
                return false;
            }

            foreach (var rank in CardRanks.Keys)
            {
                if (Cards.Count(_ => _ == rank) == 3)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsTwoPair()
        {
            if (Cards.Distinct().Count() != 3)
            {
                return false;
            }

            foreach (var rank in CardRanks.Keys)
            {
                if (Cards.Count(_ => _ == rank) > 2)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOnePair()
        {
            return Cards.Distinct().Count() == 4;
        }

        public bool IsHighCard()
        {
            return Cards.Distinct().Count() == 5;
        }

        public bool IsHigherWithJokers(Part1Hand other)
        {
            for (int ii = 0; ii < Cards.Length; ii++)
            {
                if (Cards[ii] == 'J' && other.Cards[ii] != 'J')
                {
                    return true;
                }
                else if (Cards[ii] != 'J' && other.Cards[ii] == 'J')
                {
                    return false;
                }
            }

            throw new Exception("Cards are the same");
        }
    }

    private class Part2Hand
    {
        public static Dictionary<char, char> CardRanks = new()
        {
            {'2', '2' },
            { '3', '3' },
            { '4', '4' },
            { '5', '5' },
            { '6', '6' },
            { '7', '7' },
            { '8', '8' },
            { '9', '9' },
            { 'T', 'a' },
            { 'J', 'b' },
            { 'Q', 'c' },
            { 'K', 'd' },
            { 'A', 'e'}
        };

        // five chars - five cards
        public string Cards;
        public int Bid;

        public Part2Hand(string cards, int bid)
        {
            Cards = cards.ToUpper();
            Bid = bid;
        }

        public int GetKey()
        {
            char[] score = ['0', '0', '0', '0', '0', '0']; // type, first card, second card, third card, fourth card, fifth card

            if (IsFiveOfAKind())
            {
                score[0] = '7';
            }
            else if (IsFourOfAKind())
            {
                score[0] = '6';
            }
            else if (IsFullHouse())
            {
                score[0] = '5';
            }
            else if (IsThreeOfAKind())
            {
                score[0] = '4';
            }
            else if (IsTwoPair())
            {
                score[0] = '3';
            }
            else if (IsOnePair())
            {
                score[0] = '2';
            }
            else if (IsHighCard())
            {
                score[0] = '1';
            }

            for (int ii = 0; ii < Cards.Length; ii++)
            {
                score[ii + 1] = CardRanks[Cards[ii]];
            }

            string hex = string.Join("", score);
            return Convert.ToInt32(hex, 16);
        }

        public bool IsFiveOfAKind()
        {
            foreach (var rank in CardRanks.Keys)
            {
                string fakeHand = Cards.Replace('J', rank);
                if (fakeHand.Distinct().Count() == 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsFourOfAKind()
        {
            foreach (var rank in CardRanks.Keys)
            {
                string fakeHand = Cards.Replace('J', rank);
                if (fakeHand.Count(_ => _ == rank) == 4)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsFullHouse()
        {
            // 11JJ2 => 11122 3D 2J
            // 111J2 => 11122 3D 1J
            // 11J22 => 11122 3D 1J
            // 11JJJ => 11222 2D 3J
            // 111JJ => 11122 2D 2J

            if(Cards.Count(_ => _ == 'J') >= 4)
            {
                return true;
            }

            if (Cards.Distinct().Count() == 3 && Cards.Count(_ => _ == 'J') >= 1)
            {
                return true;
            }

            if (Cards.Distinct().Count() == 2 && Cards.Count(_ => _ == 'J') >= 2)
            {
                return true;
            }

            bool foundTwo = false;
            foreach (var rank in CardRanks.Keys)
            {
                if (Cards.Count(_ => _ == rank) == 2)
                {
                    foundTwo = true;
                }
            }

            if (foundTwo)
            {
                foreach (var rank in CardRanks.Keys)
                {
                    if (Cards.Count(_ => _ == rank) == 3)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsThreeOfAKind()
        {
            // 12JJ3 => 12223
            // 122J3 => 12223
            // 11JJ2 => 11132
            // 111J2 => 11132
            // 1JJJJ => 11123

            if(Cards.Count(_ => _ == 'J') >= 3)
            {
                return true;
            }

            if (Cards.Distinct().Count() == 4 && Cards.Count(_ => _ == 'J') == 2)
            {
                return true;
            }

            if (Cards.Distinct().Count() == 3 && Cards.Count(_ => _ == 'J') <= 2)
            {
                return true;
            }

            if (Cards.Distinct().Count() != 3)
            {
                return false;
            }

            foreach (var rank in CardRanks.Keys)
            {
                string fakeHand = Cards.Replace('J', rank);
                if (fakeHand.Count(_ => _ == rank) == 3)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsTwoPair()
        {
            // 11JJ2 => 11232
            // 11J22 => 11322
            // 112JJ => 11132
            // 123JJ => 12323
            // 1233J => 12332

            if(Cards.Count(_ => _ == 'J') >= 3)
            {
                return true;
            }

            if (Cards.Distinct().Count() == 3 && Cards.Count(_ => _ == 'J') == 2)
            {
                return true;
            }

            if (Cards.Distinct().Count() == 4 && Cards.Count(_ => _ == 'J') == 2)
            {
                return true;
            }

            if (Cards.Where(_ => _ != 'J').Distinct().Count() == 3 && Cards.Count(_ => _ == 'J') == 1)
            {
                return true;
            }

            if (Cards.Distinct().Count() != 3)
            {
                return false;
            }

            foreach (var rank in CardRanks.Keys)
            {
                if (Cards.Count(_ => _ == rank) > 2)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOnePair()
        {
            if(Cards.Contains('J'))
            {
                return true;
            }

            return Cards.Distinct().Count() == 4;
        }

        public bool IsHighCard()
        {
            return true;
        }

        public bool IsHigherWithJokers(Part2Hand other)
        {
            for (int ii = 0; ii < Cards.Length; ii++)
            {
                if (Cards[ii] == 'J' && other.Cards[ii] != 'J')
                {
                    return true;
                }
                else if (Cards[ii] != 'J' && other.Cards[ii] == 'J')
                {
                    return false;
                }
            }

            throw new Exception("Cards are the same");
        }
    }
}