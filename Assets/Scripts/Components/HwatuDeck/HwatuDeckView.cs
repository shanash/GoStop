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
            cards[i].LocalPosition = new Vector3(transform.position.x, origin_y - 0.001f * i, transform.position.z);
            cards[i].State = CardState.FaceDown;
            cards[i].Show = true;

            cards[i].SetParent(this.transform);

            views.Add(cards[i].View);
        }

        return true;
    }

    public void UpdateView(List<HwatuCard> cards)
    {
        float origin_y = transform.position.y + (cards.Count * 0.001f);
        for (int i = 0; i < cards.Count; i++)
        {
            HwatuCard card = cards[i];
            HwatuCardView view = cards[i].View;

            card.SetParent(this.transform);

            card.LocalPosition = new Vector3(transform.position.x, origin_y - 0.001f * i, transform.position.z);
            card.State = CardState.FaceDown;
            card.Show = true;

            // Hierarchy 순서를 List 순서에 맞게 조정
            view.transform.SetSiblingIndex(i);
        }
    }

    public HwatuDeckView Instantiate(Transform parent = null)
    {
        return Instantiate(this, parent);
    }
}
