using ToyLets.Util;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    GameManager() { }

    protected override void Initialize()
    {
        HwatuDeck d = new HwatuDeck();

        d.Shuffle();

        foreach (var c in d.Cards)
        {
            Debug.Log($"{c.Month} {c.Type}");
        }

        d.Show = true;
    }
}
