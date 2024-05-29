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

    public void OnClickPop()
    {
        var card = GameManager.I.Deck.Pop();
        card.Position = new Vector3(100, 100, 100);
    }

    public void OnClickShuffle()
    {
        GameManager.I.Deck.Shuffle();
    }

    public void OnClickDraw()
    {
        GameManager.I.Deck.Pop();
    }
}
