using UnityEngine;

public class Player
{
    public PlayerModel model;
    public PlayerView view;

    public Player()
    {
        model = new PlayerModel();
        var origin = Resources.Load<PlayerView>("Prefabs/Player");
        view = origin.Instantiate();
    }

    public void Add(HwatuCard card)
    {
        model.Add(card);
        view.UpdateView(model.Cards);
    }
}
