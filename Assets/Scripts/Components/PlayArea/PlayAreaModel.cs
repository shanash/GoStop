using System.Collections.Generic;

public class PlayAreaModel
{
    public List<HwatuCard> Cards = new List<HwatuCard>();
    public PlayAreaModel()
    {

    }

    public void Add(HwatuCard card)
    {
        Cards.Add(card);
    }
}
