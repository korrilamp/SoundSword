using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;
using UnityEngine.XR;

public class drawController : MonoBehaviour
{
    public GameObject prefab;
    public bool FirstPressed;
    public static bool IsPlaying;

    private bool recordStarted;
    private GameObject trail;
    private GameObject replayTrail;
    private Vector3 startPos;
    private int replayFrameIndex;
    // private List<InputDevice> allDevices = new List<InputDevice>();
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    public static AudioSource musicTrack;


    // Start is called before the first frame update
    void Start()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);
        FirstPressed = true;
        IsPlaying = false;
        //recordStarted = false;
        musicTrack = GetComponent<AudioSource>(); //retrieve audio
    }

    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);

        if (rightHandDevices.Count > 0)
        {
            //Drawing function
            bool triggerValue;
            if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
            {
                if (FirstPressed) //if it is the first time being pressed
                {
                    if (IsPlaying) { //if music is playing, pause
                    	musicTrack.Pause();
                        if (RecordManager.recordingInitialized) { //if currently recording, pause (not stop) the recording
                            RecordManager.recording = false;
                            Debug.Log("PAUSE Music and Recording");
                        }
                    } else { //if music is paused, play and start drawing
                        musicTrack.Play();
                        if (RecordManager.recordingInitialized) { //if currently recording, play the recording
                            RecordManager.recording = true;
                            if (RecordManager.waitingToRecord)
                            { //if recording has not been initiated, create new recording array
                                RecordManager.InitiateRecording();
                                RecordManager.waitingToRecord = false;
                                Debug.Log("INITIATE Recording");
                            }
                            else {
                                Debug.Log("RESUME Music and Recording");
                            }
                        }
                        drawDown();
                    }
                    FirstPressed = false;
                    IsPlaying = !IsPlaying;
                }

            	//during press and music is playing
                else if (!FirstPressed && IsPlaying) {
                	drawDuring();
                }
            }
 
            // button release and music playing
            else if (IsPlaying)
            {
                FirstPressed = true;
                drawDuring();
            }

            // button release and music paused
            else if (!FirstPressed && !IsPlaying)
            {
                FirstPressed = true;
                musicTrack.Pause();
                drawDuring();
            }
        }
    }
    Vector2 GetJoystickValue()
    {
        Vector2 joyValue = Vector2.zero;
        Vector2 primary2DValue;
        InputFeatureUsage<Vector2> primary2DVector = CommonUsages.primary2DAxis;
        if (rightHandDevices[0] != null && (rightHandDevices[0].TryGetFeatureValue(primary2DVector, out primary2DValue) && primary2DValue != Vector2.zero))
        {
            joyValue = primary2DValue;
        } 
        return joyValue;
    }

    void drawDown()
    {
        trail = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);
        RecordManager.UpdateRecording(transform.position, transform.rotation, GetJoystickValue());
        //Debug.Log("the joystick value is" + GetJoystickValue());
    }
    void drawDuring()
    {
        trail.transform.position = transform.position;
        RecordManager.UpdateRecording(transform.position, transform.rotation, GetJoystickValue());
        //Debug.Log("the joystick value is" + GetJoystickValue());
        //float track_volume = transform.position.y + .5f;
        //Debug.Log(track_volume);
        //Debug.Log(transform.position);
          //  if (track_volume > 1)
            //{
            //musicTrack.volume = 1;
            //}
            //else if (track_volume < 0){
            //musicTrack.volume = 0;
              //  }
           // else{
           // musicTrack.volume = track_volume;
            // }

        
    }
}
