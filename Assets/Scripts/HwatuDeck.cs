using System.Collections.Generic;
using UnityEngine;

public class HwatuDeck
{
    public HwatuDeckModel deckModel;
    public HwatuDeckView deckView;
    List<HwatuCard> _cards = null;


    public bool Show
    {
        get; set;
    }

    public HwatuDeck(List<HwatuCard> cards)
    {
        _cards = cards;

        deckModel = new HwatuDeckModel(cards);
        deckModel.Shuffle();

        var origin = Resources.Load<HwatuDeckView>("Prefabs/Deck");
        deckView = origin.Instantiate();
        deckView.Initialize(cards);
    }

    public void Shuffle()
    {
        deckModel.Shuffle();
        deckView.UpdateView(deckModel.Cards);
    }

    public void FlipFirst()
    {
        _cards[0].Flip();
    }

    public void Pop()
    {

    }
}
