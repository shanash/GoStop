using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _ = GameManager.Instance;
    }

    // Update is called once per frame
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
        GameManager.I.Draw();
    }
}
