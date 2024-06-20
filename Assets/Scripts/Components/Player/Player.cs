using System;
using UnityEngine;

public class Player
{
    PlayerModel model = null;
    PlayerView view = null;

    // 델리게이트와 이벤트를 정의합니다.
    public event Action<HwatuCard> OnCardPlayed;

    public Player(Transform parent, bool isLocalPlayer = false)
    {
        model = new PlayerModel();
        model.IsLocalPlayer = isLocalPlayer;

        var origin = Resources.Load<PlayerView>("Prefabs/Player");
        view = origin.Instantiate(parent);
    }

    public void Draw(int number = 1)
    {
        for (int i = 0; i < number; i++)
        {
            var card = GameManager.I.Deck.Pop();
            if (model.IsLocalPlayer)
            {
                card.View.gameObject.layer = 7;
            }
            model.Add(card);
        }
        view.UpdateView(model);
    }

    public void PlayCard()
    {
        HwatuCard card = model.SelectedCard;
        OnCardPlayed?.Invoke(card);
        view.Remove(card.View);
        model.Cards.Remove(card);
        card.View.Initialze();

        view.UpdateView(model);
    }

    public void SelectCard(GameObject go)
    {
        int index = -1;
        for (int i = 0; i < model.Cards.Count; i++)
        {
            if (model.Cards[i].View.gameObject == go)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            return;

        HwatuCard card = model.Cards[index];

        model.Select(card);
        view.Select(card.View);
    }

    public void ResetSelectedCard()
    {
        model.Select(null);
        view.ResetSelected();
    }
}
