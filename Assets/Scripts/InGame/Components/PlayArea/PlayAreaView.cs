using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAreaView : MonoBehaviour
{
    [SerializeField]
    private GameObject _plane = null;

    [SerializeField]
    private List<GameObject> _playAreaPoint = null;

    public int CountPlayAreaPoint => _playAreaPoint.Count;
    private List<HwatuCardView> _views { get; set; }

    void Start()
    {
        _views = new List<HwatuCardView>();
    }

    public void UpdateView(PlayAreaModel model)
    {
        _views = new List<HwatuCardView>();
        Dictionary<int, List<HwatuCard>> cards = model.Cards;

        int areaIndex;

        foreach (var kvCard in cards)
        {
            areaIndex = kvCard.Key;
            var listCard = kvCard.Value;

            for (int i = 0; i < listCard.Count; i++)
            {
                if (_playAreaPoint.Contains(listCard[i].View.transform.parent.gameObject))
                {
                    continue;
                }
                listCard[i].SetParent(_playAreaPoint[areaIndex].transform);

                listCard[i].LocalPosition = Vector3.zero + i * new Vector3(0.04f, 0.001f, 0.04f);
                listCard[i].State = CardState.FaceUp;
                listCard[i].Show = true;
            }
        }
    }
}
