using ToyLets.Util;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameManager() { }

    protected override void Initialize()
    {
        HwatuDeck d = new HwatuDeck();

        d.Shuffle();

        d.Show = true;
    }
}
