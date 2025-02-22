using System.Collections.Generic;
using UnityEngine;

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
