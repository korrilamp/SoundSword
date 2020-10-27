using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class noShadows : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        GetComponent<Renderer>().receiveShadows = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
