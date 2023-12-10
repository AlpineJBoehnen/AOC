namespace AdventOfCode.Y2023;

public class Day7 : AdventOfCodeDay
{
    public Day7() : base(2023, 7)
    {
    }

    protected override string SolvePart1(string[] input)
    {
        SortedList<int, Hand> hands = new();

        foreach (string line in input)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Hand hand = new(parts[0], int.Parse(parts[1]));
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
        SortedList<int, SortedList<int, Hand>> hands = new();

        int kk = 0;
        foreach (string line in input)
        {
            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Hand hand = new(parts[0], int.Parse(parts[1]));
            int jackKey = hand.GetKey(true);
            hands.TryAdd(jackKey, []);
            hands[jackKey].Add(hand.GetKey(), hand);
        }

        ulong total = 0;
        int pos = 1;
        for (int ii = 0; ii < hands.Count; ii++)
        {
            for (int jj = 0; jj < hands.GetValueAtIndex(ii).Count; jj++)
            {
                total += (ulong)hands.GetValueAtIndex(ii).GetValueAtIndex(jj).Bid * (ulong)(pos++);
            }
        }

        return total.ToString();
    }

    private class Hand
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

        public static Dictionary<char, char> JackCardRanks = new()
        {
            { 'J', '1' },
            {'2', '2' },
            { '3', '3' },
            { '4', '4' },
            { '5', '5' },
            { '6', '6' },
            { '7', '7' },
            { '8', '8' },
            { '9', '9' },
            { 'T', 'a' },
            { 'Q', 'b' },
            { 'K', 'c' },
            { 'A', 'd'}
        };

        // five chars - five cards
        public string Cards;
        public int Bid;

        public Hand(string cards, int bid)
        {
            Cards = cards.ToUpper();
            Bid = bid;
        }

        public int GetKey(bool specialJackRule = false)
        {
            char[] score = ['0', '0', '0', '0', '0', '0']; // type, first card, second card, third card, fourth card, fifth card

            if (specialJackRule)
            {
                char bestType = '0';

                if(Cards.Count(_ => _ == 'J') >= 4)
                {
                    bestType = '7';
                }
                else
                {
                    string[] hands = GetJackAlternatives();

                    if (Cards == "AAJAJ" || Cards == "JJJJJ")
                    {
                        int i = 0;
                    }

                    foreach (string hand in hands)
                    {
                        if (IsFiveOfAKind(hand))
                        {
                            bestType = '7';
                            break;
                        }
                        else if (IsFourOfAKind(hand))
                        {
                            bestType = '6' > bestType ? '6' : bestType;
                        }
                        else if (IsFullHouse(hand))
                        {
                            bestType = '5' > bestType ? '5' : bestType;
                        }
                        else if (IsThreeOfAKind(hand))
                        {
                            bestType = '4' > bestType ? '4' : bestType;
                        }
                        else if (IsTwoPair(hand))
                        {
                            bestType = '3' > bestType ? '3' : bestType;
                        }
                        else if (IsOnePair(hand))
                        {
                            bestType = '2' > bestType ? '2' : bestType;
                        }
                        else if (IsHighCard(hand))
                        {
                            bestType = '1' > bestType ? '1' : bestType;
                        }
                    }
                }

                score[0] = bestType;
            }
            else
            {
                if (IsFiveOfAKind(Cards))
                {
                    score[0] = '7';
                }
                else if (IsFourOfAKind(Cards))
                {
                    score[0] = '6';
                }
                else if (IsFullHouse(Cards))
                {
                    score[0] = '5';
                }
                else if (IsThreeOfAKind(Cards))
                {
                    score[0] = '4';
                }
                else if (IsTwoPair(Cards))
                {
                    score[0] = '3';
                }
                else if (IsOnePair(Cards))
                {
                    score[0] = '2';
                }
                else if (IsHighCard(Cards))
                {
                    score[0] = '1';
                }
            }

            for (int ii = 0; ii < Cards.Length; ii++)
            {
                if (specialJackRule)
                {
                    score[ii + 1] = JackCardRanks[Cards[ii]];
                }
                else
                {
                    score[ii + 1] = CardRanks[Cards[ii]];
                }
            }

            if(Cards == "AAJAJ" || Cards == "JJJJJ")
            {
                int i = 0;
            }

            string hex = string.Join("", score);
            return Convert.ToInt32(hex, 16);
        }

        public string[] GetJackAlternatives()
        {
            List<string> hands = [];
            hands.Add(Cards);

            if (!Cards.Contains('J'))
            {
                return hands.ToArray();
            }

            // Generate permutations of Cards with Jokers replaced with every other card
            for (int ii = 0; ii < Cards.Length; ii++)
            {
                List<string> handsForCurrentIndex = new();
                if (Cards[ii] == 'J')
                {
                    foreach (string hand in hands)
                    {
                        foreach (var rank in CardRanks.Keys)
                        {
                            char[] newHand = hand.ToCharArray();
                            newHand[ii] = rank;
                            string newHandString = string.Join("", newHand);
                            if (!hands.Contains(newHandString))
                            {
                                handsForCurrentIndex.Add(string.Join("", newHand));
                            }
                        }
                    }
                }

                hands.AddRange(handsForCurrentIndex);
            }

            return hands.ToArray();
        }
        public bool IsHigherWithJokers(Hand other)
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

        public static bool IsFiveOfAKind(string hand)
        {
            return hand.Distinct().Count() == 1;
        }

        public static bool IsFourOfAKind(string hand)
        {
            foreach (var rank in Hand.CardRanks.Keys)
            {
                if (hand.Count(_ => _ == rank) == 4)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsFullHouse(string hand)
        {
            bool foundTwo = false;
            foreach (var rank in Hand.CardRanks.Keys)
            {
                if (hand.Count(_ => _ == rank) == 2)
                {
                    foundTwo = true;
                }
            }

            if (!foundTwo)
            {
                return false;
            }

            foreach (var rank in Hand.CardRanks.Keys)
            {
                if (hand.Count(_ => _ == rank) == 3)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsThreeOfAKind(string hand)
        {
            if (hand.Distinct().Count() != 3)
            {
                return false;
            }

            foreach (var rank in Hand.CardRanks.Keys)
            {
                if (hand.Count(_ => _ == rank) == 3)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsTwoPair(string hand)
        {
            if (hand.Distinct().Count() != 3)
            {
                return false;
            }

            foreach (var rank in Hand.CardRanks.Keys)
            {
                if (hand.Count(_ => _ == rank) > 2)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool IsOnePair(string hand)
        {
            return hand.Distinct().Count() == 4;
        }

        public static bool IsHighCard(string hand)
        {
            return hand.Distinct().Count() == 5;
        }
    }
}