using UnityEngine;

public class Player
{
    public PlayerModel model;
    public PlayerView view;

    public Player(Transform parent)
    {
        model = new PlayerModel();
        var origin = Resources.Load<PlayerView>("Prefabs/Player");
        view = origin.Instantiate(parent);
    }

    public void Add(HwatuCard card)
    {
        model.Add(card);
        view.UpdateView(model.Cards);
    }

    public void Draw(int number = 1)
    {
        for (int i = 0; i < number; i++)
        {
            var card = GameManager.I.Deck.Pop();
            Add(card);
        }
    }
}
