using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaView : MonoBehaviour
{
    [SerializeField]
    GameObject[] PlayAreaPoint = null;

    List<HwatuCardView> views = null;

    void Start()
    {
        views = new List<HwatuCardView>();
    }

    public void UpdateView(PlayAreaModel model)
    {
        views = new List<HwatuCardView>();
        List<HwatuCard> cards = model.Cards;

        for (int i = 0; i < PlayAreaPoint.Length; i++)
        {
            if (views.Count > i && views[i] == cards[i].View)
            {
                continue;
            }

            HwatuCard card = cards[i];
            HwatuCardView view = cards[i].View;

            card.SetParent(PlayAreaPoint[i].transform);

            card.LocalPosition = Vector3.zero;
            card.State = CardState.FaceUp;
            card.Show = true;
        }
    }
}
