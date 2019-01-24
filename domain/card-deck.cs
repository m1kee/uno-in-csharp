using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;


public class CardDeck
{
    public List<Card> Cards { get; set; }

    public CardDeck()
    {
        Cards = new List<Card>();

        //For every color we have defined
        foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            if (color == CardColor.Wild)
            {
                //Add four regular Wild Cards and Draw Four Wild cards
                for (int i = 1; i <= 4; i++)
                {
                    Cards.Add(new Card()
                    {
                        Color = color,
                        Value = CardValue.Wild,
                        Score = 50
                    });

                    Cards.Add(new Card()
                    {
                        Color = color,
                        Value = CardValue.DrawFour,
                        Score = 50
                    });
                }

                continue;
            }

            foreach (CardValue val in Enum.GetValues(typeof(CardValue)))
            {
                switch (val)
                {
                    case CardValue.One:
                    case CardValue.Two:
                    case CardValue.Three:
                    case CardValue.Four:
                    case CardValue.Five:
                    case CardValue.Six:
                    case CardValue.Seven:
                    case CardValue.Eight:
                    case CardValue.Nine:
                        //Add two copies of each color card 1-9
                        Cards.Add(new Card()
                        {
                            Color = color,
                            Value = val,
                            Score = (int)val
                        });
                        Cards.Add(new Card()
                        {
                            Color = color,
                            Value = val,
                            Score = (int)val
                        });
                        break;
                    case CardValue.Skip:
                    case CardValue.Reverse:
                    case CardValue.DrawTwo:
                        //Add two copies per color of Skip, Reverse, and Draw Two
                        Cards.Add(new Card()
                        {
                            Color = color,
                            Value = val,
                            Score = 20
                        });
                        Cards.Add(new Card()
                        {
                            Color = color,
                            Value = val,
                            Score = 20
                        });
                        break;

                    case CardValue.Zero:
                        //Add one copy per color for 0
                        Cards.Add(new Card()
                        {
                            Color = color,
                            Value = val,
                            Score = 0
                        });
                        break;
                }
            }
        }
    }
    public void Shuffle()
    {
        Random r = new Random();

        List<Card> cards = Cards;

        for (int n = cards.Count - 1; n > 0; --n)
        {
            int k = r.Next(n + 1);
            Card temp = cards[n];
            cards[n] = cards[k];
            cards[k] = temp;
        }
    }
    public List<Card> Draw(int count)
    {
        var drawnCards = Cards.Take(count).ToList();

        //Remove the drawn cards from the draw pile
        Cards.RemoveAll(x => drawnCards.Contains(x));

        return drawnCards;
    }
}