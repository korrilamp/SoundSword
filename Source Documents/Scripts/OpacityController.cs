using UnityEngine;
using UnityEngine.UI;

public class OpacityController : MonoBehaviour
{
    Renderer rend;
    public Slider mainSlider;
    void Start()
    {
        rend = GetComponent<Renderer>();
        // rend.material.shader = Shader.Find("Custom/TransparentDiffuse ZWrite");
    }

    public void UpdateObject()
    {
        for(int i=0; i<rend.materials.Length;i++)
        {
            Vector4 temp = rend.materials[i].GetVector("_Color");
            float r = temp.x;
            float g = temp.y;
            float b = temp.z;
            rend.materials[i].SetVector("_Color", new Vector4(r, g, b, mainSlider.value));
        }
    }
}
