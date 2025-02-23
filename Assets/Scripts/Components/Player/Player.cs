using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Player
{
    // 델리게이트와 이벤트를 정의합니다.
    public event Action<HwatuCard> OnCardPlayed;

    public bool IsLocal => _model.IsLocalPlayer;
    public bool PlayedCard { get;  private set; }
    private PlayerModel _model { get; set; } = null;
    private PlayerView _view { get; set; } = null;

    public Player(Transform parent, bool isLocalPlayer = false)
    {
        _model = new PlayerModel();
        _model.IsLocalPlayer = isLocalPlayer;

        var origin = Resources.Load<PlayerView>("Prefabs/Player");
        _view = origin.Instantiate(parent);
    }

    public void Draw(HwatuDeck deck, int number = 1)
    {
        for (int i = 0; i < number; i++)
        {
            var card = deck.Pop();
            if (_model.IsLocalPlayer)
            {
                card.View.gameObject.layer = 7;
            }
            _model.Add(card);
        }
        _view.UpdateView(_model);
    }

    public async UniTaskVoid PlayCard()
    {
        PlayedCard = true;

        HwatuCard card = _model.SelectedCard;
        _model.Select(null);
        OnCardPlayed?.Invoke(card);
        _view.Remove(card.View);
        _model.HandCards.Remove(card);
        card.View.Initialze();

        _view.UpdateView(_model);

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        HwatuCard flippedCard = InGame.I.Deck.FlipFirst();

        await UniTask.Delay(TimeSpan.FromSeconds(1));

        InGame.I.Deck.Pop();
        OnCardPlayed?.Invoke(flippedCard);
    }

    public void SelectCard(GameObject go)
    {
        int index = -1;
        for (int i = 0; i < _model.HandCards.Count; i++)
        {
            if (_model.HandCards[i].View.gameObject == go)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return;

        HwatuCard card = _model.HandCards[index];

        _model.Select(card);
        _view.Select(card.View);
    }

    public bool IsSelectedCard()
    {
        return _model.SelectedCard != null;
    }

    public void ResetSelectedCard()
    {
        _model.Select(null);
        _view.ResetSelected();
    }

    public void AddScore(HwatuCard card)
    {
        _model.AddScoreCard(card);
    }

    public int GetScore()
    {
        int score = 0;
        var listKwang = _model.GetScoreCards(CardType.Kwang);

        switch (listKwang.Count)
        {
            case 0:
            case 1:
            case 2:
                Debug.Log($"광이 {listKwang.Count}장이라 점수가 나지 않습니다");
                break;
            case 3:
                score += 3;
                if (_model.GetScoreCards(CardType.BiKwang).Count > 0)
                {
                    score -= 1;
                }
                break;
            case 4:
                score += 4;
                break;
            case 5:
                score += 15;
                break;
            default:
                Debug.LogError($"획득한 광이 너무 많거나 마이너스입니다 : {listKwang.Count}");
                return 0;
        }

        // TODO: 점수계산 나중에 추가

        return score;
    }
}
