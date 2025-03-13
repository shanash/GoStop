using System.Collections.Generic;
using System.Linq;
using Unity.Collections;

public class PlayArea
{
    private PlayAreaModel _model { get; set; }
    private PlayAreaView _view { get; set; }

    public PlayArea(PlayAreaView view)
    {
        this._view = view;
        _model = new PlayAreaModel(_view.CountPlayAreaPoint);
    }

    public void OnCardPlayed(HwatuCard card)
    {
        // 바닥 모델에서 파라메터와 같은 월의 패가 있는지 체크
        if (_model.IsSameMonth(card))
        {
            // 같은 월의 패에 겹쳐서 때린다
            _model.AttackCard(card);
        }
        // 그런거 없으면 그냥 바닥에 놓는다
        else
        {
            PlaceCard(card);
        }

        _view.UpdateView(_model);
    }

    public void DisplayCards(HwatuDeck deck, int number)
    {
        for (int i = 0; i < number; i++)
        {
            var card = deck.Pop();
            _model.AddNew(card);
        }
        _view.UpdateView(_model);
    }

    public void PlaceCard(HwatuCard card)
    {
        _model.AddNew(card);
    }

    public List<HwatuCard> PopEarnedCards()
    {
        var earnedCards = _model.Cards
            .Where(kv => kv.Value.Count % 2 == 0)
            .ToList();

        var cards = earnedCards.SelectMany(kv => kv.Value).ToList();

        foreach (var kv in earnedCards)
        {
            _model.Cards.Remove(kv.Key);
        }

        _view.UpdateView(_model);
        
        return cards;
    }
}
