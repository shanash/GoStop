using System.Collections.Generic;

public class PlayerModel
{
    public bool IsLocalPlayer = false;
    public List<HwatuCard> Cards = new List<HwatuCard>();
    public HwatuCard SelectedCard = null;

    public void Add(HwatuCard card)
    {
        Cards.Add(card);
    }

    public void Select(HwatuCard card)
    {
        SelectedCard = card;
    }
}
