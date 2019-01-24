using Domain.Common;

public class PlayerTurn
{
    public Card Card { get; set; }
    public CardColor DeclaredColor { get; set; } //Used mainly for Wild cards
    public TurnResult Result { get; set; }

    
}