using Domain.Common;

public class Card
{
    public CardColor Color { get; set; }
    public CardValue Value { get; set; }
    public int Score { get; set; }

    public string DisplayValue
    {
        get
        {
            if (Value == CardValue.Wild)
            {
                return Value.ToString();
            }
            return Color.ToString() + " " + Value.ToString();
        }
    }
}