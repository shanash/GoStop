using UnityEngine;

public enum CardType
{
    None = 0,
    Kwang,
    Yeolggot,
    Tti,
    SsangPi,
    Pi,
}

public enum CardState
{
    FaceUp,
    FaceDown
}

public class HwatuCardModel
{
    public int Month { get; set; } = 0;
    public CardType Type { get; set; } = CardType.None;
    public string Design { get; set; } = string.Empty;

    public HwatuCardView View => view;
    HwatuCardView view = null;

    public HwatuCardModel(int month, CardType type, string design)
    {
        Month = month;
        Type = type;
        Design = design;
    }

    public void Release()
    {

    }
}
