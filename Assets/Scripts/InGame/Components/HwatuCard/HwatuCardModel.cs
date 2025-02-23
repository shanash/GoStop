using UnityEngine;

[System.Flags]
public enum CardType
{
    None = 0,
    Kwang = 1 << 0,
    BiKwang = 1 << 1,
    Yeolggot = 1 << 2,
    Tti = 1 << 3,
    Hongdan = 1 << 4,
    Chodan = 1 << 5,
    Cheongdan = 1 << 6,
    SsangPi = 1 << 7,
    Pi = 1 << 8,
}

public enum CardState
{
    FaceUp,
    FaceDown
}

public class HwatuCardModel
{
    public int Month { get; private set; } = 0;
    public CardType Type { get; private set; } = CardType.None;
    public string Design { get; private set; } = string.Empty;

    public HwatuCardModel(int month, CardType type, string design)
    {
        Month = month;
        Type = type;
        Design = design;
    }
}
