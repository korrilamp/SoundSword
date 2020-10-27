using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameOffsetter : MonoBehaviour
{
    public int frameOffset = 0;
    // Start is called before the first frame update
    void Start()
    {
        var videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.frame = 0 + frameOffset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
