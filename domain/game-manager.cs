using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

public class GameManager
{
    public List<Player> Players { get; set; }
    public CardDeck DrawPile { get; set; }
    public List<Card> DiscardPile { get; set; }

    public GameManager(int numPlayers)
    {
        Players = new List<Player>();
        DrawPile = new CardDeck();
        DrawPile.Shuffle();

        //Create the players
        for (int i = 1; i <= numPlayers; i++)
        {
            Players.Add(new Player()
            {
                Position = i
            });
        }

        int maxCards = 7 * Players.Count;
        int dealtCards = 0;

        //Deal 7 cards to each player
        while (dealtCards < maxCards)
        {
            for (int i = 0; i < numPlayers; i++)
            {
                Players[i].Hand.Add(DrawPile.Cards.First());
                DrawPile.Cards.RemoveAt(0);
                dealtCards++;
            }
        }

        //Add a single card to the discard pile
        DiscardPile = new List<Card>();
        DiscardPile.Add(DrawPile.Cards.First());
        DrawPile.Cards.RemoveAt(0);

        //Game rules do not allow the first discard to be a wild.
        while (DiscardPile.First().Value == CardValue.Wild || DiscardPile.First().Value == CardValue.DrawFour)
        {
            DiscardPile.Insert(0, DrawPile.Cards.First());
            DrawPile.Cards.RemoveAt(0);
        }

        //And now we're ready to play!
    }
    public void PlayGame()
    {
        int i = 0;
        bool isAscending = true;

        //First, let's show what each player starts with
        foreach (var player in Players)
        {
            player.ShowHand();
        }

        //Game won't start until user presses Enter
        Console.ReadLine();

        //We need a "mock" PlayerTurn representing the first discard
        PlayerTurn currentTurn = new PlayerTurn()
        {
            Result = TurnResult.GameStart,
            Card = DiscardPile.First(),
            DeclaredColor = DiscardPile.First().Color
        };

        Console.WriteLine("First card is a " + currentTurn.Card.DisplayValue + ".");

        //Game continues until somebody has no cards in their hand
        while (!Players.Any(x => !x.Hand.Any()))
        {
            //If the draw pile is getting low, shuffle the discard pile into the draw pile
            if (DrawPile.Cards.Count < 4)
            {
                var currentCard = DiscardPile.First();

                //Take the discarded cards, shuffle them, and make them the new draw pile.
                DrawPile.Cards = DiscardPile.Skip(1).ToList();
                DrawPile.Shuffle();

                //Reset the discard pile to only have the current card.
                DiscardPile = new List<Card>();
                DiscardPile.Add(currentCard);

                Console.WriteLine("Shuffling cards!");
            }

            //Now the current player can take their turn
            var currentPlayer = Players[i];
            currentTurn = Players[i].PlayTurn(currentTurn, DrawPile);

            //We must add the current player's discarded card to the discard pile.
            AddToDiscardPile(currentTurn);

            //When somebody plays a reverse card, we need to reverse the turn order
            if (currentTurn.Result == TurnResult.Reversed)
            {
                isAscending = !isAscending;
            }

            //Now we figure out who has the next turn.
            if (isAscending)
            {
                i++;
                if (i >= Players.Count) //Reset player counter
                {
                    i = 0;
                }
            }
            else
            {
                i--;
                if (i < 0)
                {
                    i = Players.Count - 1;
                }
            }
        }

        //Let's see who won the game!
        var winningPlayer = Players.Where(x => !x.Hand.Any()).First();
        Console.WriteLine("Player " + winningPlayer.Position.ToString() + " wins!!");

        //Finally, calculate and display each player's score
        foreach (var player in Players)
        {
            Console.WriteLine("Player " + player.Position.ToString() + " has " + player.Hand.Sum(x => x.Score).ToString() + " points in his hand.");
        }
    }
    private void AddToDiscardPile(PlayerTurn currentTurn)
    {
        if (currentTurn.Result == TurnResult.PlayedCard
            || currentTurn.Result == TurnResult.DrawTwo
            || currentTurn.Result == TurnResult.Skip
            || currentTurn.Result == TurnResult.WildCard
            || currentTurn.Result == TurnResult.WildDrawFour
            || currentTurn.Result == TurnResult.Reversed)
        {
            DiscardPile.Insert(0, currentTurn.Card);
        }
    }
}
