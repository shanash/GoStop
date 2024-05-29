using UnityEngine;

public class HwatuCard
{
    HwatuCardModel _model;
    HwatuCardView _view;

    public HwatuCardModel Model { get => _model; }
    public HwatuCardView View { get => _view; }

    public Vector3 Position
    {
        get
        {
            return position;
        }
        set
        {
            if (_view != null)
            {
                Debug.Log($"{_view.name} : {value}");
                _view.transform.position = value;
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
            _view.gameObject.SetActive(value);
            show = value;
        }
    }
    bool show = true;


    public CardState State
    {
        get => state;
        set
        {
            _view?.SetFace(value);
            state = value;
        }
    }
    CardState state = CardState.FaceDown;

    // 기본값이 있는 생성자
    public HwatuCard(int month, CardType type, string design)
        : this(month, type, design, new Vector3(0, 1, 0))
    {
    }

    public HwatuCard(int month, CardType type, string design, Vector3 pos)
    {
        _model = new HwatuCardModel(month, type, design);

        var origin = Resources.Load<HwatuCardView>("Prefabs/Card");
        _view = Object.Instantiate(origin);
        _view.name = $"{origin.name}_{month}_{type}";
        _view.transform.position = pos;
        _view.Design = _model.Design;
        _view.SetFace(State);
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
        _view.transform.SetParent(parent);
    }
}
