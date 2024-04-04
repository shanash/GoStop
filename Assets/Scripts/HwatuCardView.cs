using UnityEngine;

public class HwatuCardView : MonoBehaviour
{
    [SerializeField]
    Material source = null;
    [SerializeField]
    MeshRenderer mr = null;

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

    void SetDesign(string designName)
    {
        Debug.Log($"D : {designName}");
        var mat = new Material(source);
        var path = $"Images/{designName}";
        Debug.Log($"path : {path}");
        var tex = Resources.Load<Texture2D>($"Images/{designName}");
        mat.SetTexture("_BaseMap", tex);
        mr.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
