using UnityEngine;
using System.Collections.Generic;

public class HwatuDeckView : MonoBehaviour
{
    List<HwatuCardView> views = null;

    public bool Initialize(List<HwatuCard> cards)
    {
        views = new List<HwatuCardView>();

        float origin_y = transform.position.y + (cards.Count * 0.001f);
        Debug.Log($"Cards.Count : {cards.Count}");
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].Position = new Vector3(transform.position.x, origin_y - 0.001f * i, transform.position.z);
            cards[i].State = CardState.FaceDown;
            cards[i].Show = true;

            cards[i].View.transform.parent = this.transform;

            views.Add(cards[i].View);
        }

        return true;
    }

    public void UpdateView(List<HwatuCard> cards)
    {

    }

    public HwatuDeckView Instantiate(Transform parent = null)
    {
        return Instantiate(this, parent);
    }
}
