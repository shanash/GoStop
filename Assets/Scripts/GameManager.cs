using System.Collections.Generic;
using ToyLets.GenericPatterns;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public HwatuDeck Deck = null;
    public Player LocalPlayer = null;
    public Player RemotePlayerLeft = null;
    public Player RemotePlayerRight = null;

    public PlayArea Area = null;

    GameManager() { }

    protected override void Initialize()
    {

    }
}
