using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField]
    Transform localPlayer = null;

    [SerializeField]
    Transform leftPlayer = null;

    [SerializeField]
    Transform rightPlayer = null;

    void Start()
    {
        _ = GameManager.Instance;
        GameManager.I.InitPlayers(localPlayer, leftPlayer, rightPlayer);
    }

    void Update()
    {
        
    }

    public void OnClickFlipFirst()
    {
        GameManager.I.Deck.FlipFirst();
    }

    public HwatuCard OnClickPop()
    {
        return GameManager.I.Deck.Pop();
    }

    public void OnClickShuffle()
    {
        GameManager.I.Deck.Shuffle();
    }

    public void OnClickDraw()
    {
        //GameManager.I.Draw();
    }
}
