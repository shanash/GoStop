using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaView : MonoBehaviour
{
    [SerializeField]
    GameObject Plane = null;

    [SerializeField]
    List<GameObject> PlayAreaPoint = null;

    List<HwatuCardView> views = null;

    void Start()
    {
        views = new List<HwatuCardView>();
    }

    public void UpdateView(PlayAreaModel model)
    {
        views = new List<HwatuCardView>();
        List<HwatuCard> cards = model.Cards;

        int areaIndex = -1;



        for (int i = 0; i < cards.Count; i++)
        {
            if (PlayAreaPoint.Contains(cards[i].View.transform.parent.gameObject))
            {
                continue;
            }

            for (int j = areaIndex+1; j < PlayAreaPoint.Count; j++)
            {
                if (PlayAreaPoint[j].transform.childCount == 0)
                {
                    areaIndex = j;
                    break;
                }
            }

            HwatuCard card = cards[i];
            card.SetParent(PlayAreaPoint[areaIndex].transform);

            card.LocalPosition = Vector3.zero;
            card.State = CardState.FaceUp;
            card.Show = true;

        }
    }
}
