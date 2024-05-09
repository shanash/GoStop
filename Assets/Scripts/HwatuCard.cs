using UnityEngine;

public class HwatuCard
{
    HwatuCardModel _cardModel;
    HwatuCardView _cardView;

    public HwatuCardModel Model { get => _cardModel; }
    public HwatuCardView View { get => _cardView; }

    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            if (_cardView != null)
            {
                _cardView.transform.position = value;
            }
            position = value;
        }
    }
    Vector3 position = Vector3.zero;

    public bool Show
    {
        get => show;
        set
        {
            if (value)
            {
                if (_cardView == null)
                {
                    var origin = Resources.Load<HwatuCardView>("Prefabs/Card");
                    _cardView = Object.Instantiate(origin);
                    _cardView.transform.position = position;
                    _cardView.Design = _cardModel.Design;
                    _cardView.SetFace(State);
                }
            }

            _cardView.gameObject.SetActive(value);

            show = value;
        }
    }
    bool show = false;


    public CardState State
    {
        get => state;
        set
        {
            _cardView?.SetFace(value);
            state = value;
        }
    }
    CardState state = CardState.FaceDown;

    public HwatuCard(int month, CardType type, string design)
    {
        _cardModel = new HwatuCardModel(month, type, design);
    }

    public void Flip()
    {
        switch (State)
        {
            case CardState.FaceDown:
                State = CardState.FaceUp;
                break;
            case CardState.FaceUp:
                State = CardState.FaceDown;
                break;
        }
    }

    public void Release()
    {
    }

    public void SetParent(Transform parent)
    {
        _cardView.transform.SetParent(parent);
    }
}
