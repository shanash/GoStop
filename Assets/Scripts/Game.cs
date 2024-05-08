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
        GameManager.I.Deck.Pop();
    }
}
