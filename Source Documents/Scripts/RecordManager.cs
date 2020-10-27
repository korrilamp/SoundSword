using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class RecordManager : MonoBehaviour
{
    public static bool recording; //current position and rotation are being recorded (on pause recording, recording = false)
    public static bool recordingInitialized; //recording initialized
    public static bool waitingToRecord; //true if music not yet playing and primary button is pressed

    private static List<List<Vector3>> allPositionRecords = new List<List<Vector3>>();
    private static List<List<Quaternion>> allRotationRecords = new List<List<Quaternion>>();
    private static List<List<Vector2>> allJoystickRecords = new List<List<Vector2>>();
    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    private bool firstRecordPress;
    private bool firstJoyPress;

    // Start is called before the first frame update
    void Start()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);
        firstRecordPress = true;
        firstJoyPress = true;
        recording = false;
        recordingInitialized = false;
        waitingToRecord = false;
    }

    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);

        if (rightHandDevices.Count > 0)
        {
            bool recordButtonValue;
            if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.primaryButton, out recordButtonValue) && recordButtonValue)
            {
                if (firstRecordPress) //if it is the first time being pressed
                {
                    if (recordingInitialized)
                    { //stop active recording (also stop music)
                        Debug.Log("PRESSED: STOP recording and STOP music");
                        recordingInitialized = false;
                        recording = false;
                        SaveLoad.Save(ReturnPositions()[0], ReturnRotations()[0], RetrunJoysticks()[0]);
                        drawController.musicTrack.Pause(); //if music playing when recording stopped, then stop music
                        drawController.IsPlaying = false; 
                    }
                    else
                    { //begin new recording
                        recordingInitialized = true;
                        if (!drawController.IsPlaying) //if music not yet playing, wait for trigger
                        {
                            Debug.Log("PRIMARY PRESSED: WAITING TO INITIATE recording");
                            waitingToRecord = true;
                        }
                        else { //if music already playing, initialize recording
                            Debug.Log("PRIMARY PRESSED: INITIATED recording");
                            InitiateRecording();
                            
                        }
                        
                    }
                    firstRecordPress = false;
                }
                //else: during press primary
            } else firstRecordPress = true;
         
            bool joystickValue;
            if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.primary2DAxisClick, out joystickValue) && joystickValue)
            {
                if (firstJoyPress)
                {
                    Debug.Log("restart music from beginning, stop and do not save recording");
                    drawController.musicTrack.Stop(); //stop audio track, next time it is played, it will start from beginning
                    drawController.IsPlaying = false;
                    recording = false; //if recording, stop and discard
                    recordingInitialized = false;
                    firstJoyPress = false;
                }


            }
            else
            {
                firstJoyPress = true;
            }
        }
    }

    public static void InitiateRecording() {
        recording = true;
        if (allPositionRecords.Count > 0) {
            allPositionRecords[0] = new List<Vector3>(); //new edit is for joystick restart, will record over old recording
            allRotationRecords[0] = new List<Quaternion>();
            allJoystickRecords[0] = new List<Vector2>();
        }
        else
        {
            allPositionRecords.Add(new List<Vector3>());
            allRotationRecords.Add(new List<Quaternion>());
            allJoystickRecords.Add(new List<Vector2>());
        }
    }

    public static void UpdateRecording(Vector3 Position, Quaternion Rotation, Vector2 Joystick) {
        if (recording)
        {
            Debug.Log("Recording now");
            allPositionRecords[allPositionRecords.Count - 1].Add(Position);
            allRotationRecords[allRotationRecords.Count - 1].Add(Rotation);
            allJoystickRecords[allJoystickRecords.Count - 1].Add(Joystick);
        }
    }

    public static List<List<Vector3>> ReturnPositions()
    {
        recording = false;
        return allPositionRecords;
    }

    public static List<List<Quaternion>> ReturnRotations()
    {
        recording = false;
        return allRotationRecords;
    }
    
    public static List<List<Vector2>> RetrunJoysticks()
    {
        recording = false;
        return allJoystickRecords;
    }
}
