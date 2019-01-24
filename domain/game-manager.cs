using Domain.Common;
using System.Collections.Generic;


public class GameManager {
    public List<Player> Players { get; set; }
    public CardDeck DrawPile { get; set; }
    public List<Card> DiscardPile { get; set; }
}