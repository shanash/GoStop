using UnityEngine;

public class HwatuCardView : MonoBehaviour
{
    [SerializeField]
    Material source = null;
    [SerializeField]
    MeshRenderer mr = null;
    [SerializeField]
    Transform pivot = null;

    public string Design
    {
        get => design;
        set
        {
            if (design.Equals(value))
            {
                return;
            }
            SetDesign(value);
            design = value;
        }
    }
    string design = string.Empty;

    public void Initialze()
    {
        transform.localScale = Vector3.one;
        transform.localEulerAngles = Vector3.zero;
    }

    void SetDesign(string designName)
    {
        var mat = new Material(source);
        var path = $"Images/{designName}";
        var tex = Resources.Load<Texture2D>(path);
        mat.SetTexture("_BaseMap", tex);
        mr.material = mat;
    }

    public void SetFace(CardState state)
    {
        pivot.localEulerAngles = new Vector3(0, 0, state == CardState.FaceDown ? 0 : 180);
    }
}
