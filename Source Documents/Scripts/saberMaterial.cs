using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saberMaterial : MonoBehaviour
{
    public List<Renderer> objectsToChange = new List<Renderer>();
    public Material desiredMaterial;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var rend in objectsToChange)
        {
            var mats = new Material[rend.materials.Length];
            for (var j = 0; j < rend.materials.Length; j++)
            {
                mats[j] = desiredMaterial;
            }
            rend.materials = mats;
        }
    }
}
