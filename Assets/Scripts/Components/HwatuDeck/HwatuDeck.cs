using System.Collections.Generic;
using UnityEngine;

public class HwatuDeck
{
    HwatuDeckModel _model;
    HwatuDeckView _view;

    public bool Show
    {
        get; set;
    }

    public HwatuDeck(List<HwatuCard> cards)
    {
        _model = new HwatuDeckModel(cards);
        _model.Shuffle();

        var origin = Resources.Load<HwatuDeckView>("Prefabs/Deck");
        _view = origin.Instantiate();
        _view.Initialize(cards);
        //_view.UpdateView(_model.Cards);
    }

    public void Shuffle()
    {
        _model.Shuffle();
        _view.UpdateView(_model.Cards);
    }

    public void FlipFirst()
    {
        _model.Cards[0].Flip();
    }

    public HwatuCard Pop()
    {
        var card = _model.Cards[0];
        _model.Cards.RemoveAt(0);
        _view.UpdateView(_model.Cards);

        return card;
    }
}
