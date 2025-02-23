using UnityEngine;

public class HwatuCard
{
    public HwatuCardModel Model { get; private set; }
    public HwatuCardView View { get; private set; }

    public Vector3 LocalPosition
    {
        get
        {
            return localPosition;
        }
        set
        {
            if (View != null)
            {
                Debug.Log($"{View.name} : {value}");
                View.transform.localPosition = value;
            }
            localPosition = value;
        }
    }
    Vector3 localPosition = Vector3.zero;

    public Quaternion LocalRotation { get
        {
            return localRotation;
        }
        set
        {
            if (View != null)
            {
                View.transform.localRotation = value;
            }
            localRotation = value;
        }
    }
    Quaternion localRotation = Quaternion.identity;

    public bool Show
    {
        get => show;
        set
        {
            View.gameObject.SetActive(value);
            show = value;
        }
    }
    bool show = true;


    public CardState State
    {
        get => state;
        set
        {
            View?.SetFace(value);
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
        Model = new HwatuCardModel(month, type, design);

        var origin = Resources.Load<HwatuCardView>("Prefabs/Card");
        View = Object.Instantiate(origin);
        View.name = $"{origin.name}_{month}_{type}";
        View.transform.position = pos;
        View.Design = Model.Design;
        View.SetFace(State);
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
        View.transform.SetParent(parent);
    }
}
