using UnityEngine;

public class Player
{
    
    PlayerModel model = null;
    PlayerView view = null;

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
            model.Add(card);
        }
        view.UpdateView(model);
    }
}
