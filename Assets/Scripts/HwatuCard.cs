using UnityEngine;

public enum CardType
{
    None = 0,
    Kwang,
    Yeolggot,
    Tti,
    Pi,
}

public enum CardState
{
    FaceUp,
    FaceDown
}

public class HwatuCard
{
    public int Month { get; set; } = 0;
    public CardType Type { get; set; } = CardType.None;
    public string Design { get; set; } = string.Empty;
    public CardState State { get; set; } = CardState.FaceDown;

    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            if (view != null)
            {
                view.transform.position = value;
            }
            position = value;
        }
    }
    Vector3 position = Vector3.zero;

    public bool Show
    {
        get => show;
        set
        {
            if (value)
            {
                if (view == null)
                {
                    var origin = Resources.Load<HwatuCardView>("Prefabs/Card");
                    view = Object.Instantiate(origin);
                    view.transform.position = position;
                    view.Design = Design;
                    view.SetFace(State);
                }
            }

            view.gameObject.SetActive(value);

            show = value;
        }
    }
    bool show = false;

    HwatuCardView view = null;

    public HwatuCard(int month, CardType type, string design)
    {
        Month = month;
        Type = type;
        Design = design;
    }
}
