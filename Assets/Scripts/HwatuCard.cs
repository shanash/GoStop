public enum CardType
{
    None,
    Kwang,
    Yeolggot,
    Tti,
    Pi,
}

public class HwatuCard
{
    public int Month { get; set; } // 1월부터 12월
    public CardType Type { get; set; } // 카드의 종류

    public HwatuCard(int month, CardType type, bool isBonus = false)
    {
        Month = month;
        Type = type;
    }
}
