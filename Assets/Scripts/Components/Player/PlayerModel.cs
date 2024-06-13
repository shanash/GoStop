using System.Collections.Generic;

public class PlayerModel
{
    public bool IsLocalPlayer = false;
    public List<HwatuCard> Cards = new List<HwatuCard>();

    public void Add(HwatuCard card)
    {
        Cards.Add(card);
    }
}
