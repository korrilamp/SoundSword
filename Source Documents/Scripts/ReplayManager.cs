using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ReplayManager : MonoBehaviour
{
    public GameObject prefab;
    public GameObject fileImportPrefab;

    private List<InputDevice> leftHandDevices = new List<InputDevice>();
    private List<InputDevice> rightHandDevices = new List<InputDevice>();
    private bool firstPrimaryButtonPress;
    private bool activeStackedReplay;
    private bool waitingForTrigger;
    private int replayFrameIndex;
    private int shortestReplayCount;
    private List<List<Vector3>> PosRecordings;
    private List<List<Quaternion>> RotRecordings;
    private List<GameObject> trailReplays = new List<GameObject>();

    // James file loading stuff
    private GameObject firstLoadedObject;
    private GameObject secondLoadedObject;
    private GameObject thirdLoadedObject;
    private int currFrame;

    // Start is called before the first frame update
    void Start()
    {
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHandDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHandDevices);
        firstPrimaryButtonPress = true;
        waitingForTrigger = false;

        currFrame = 0;
        FileReplayInit();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right, rightHandDevices);
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Left, leftHandDevices);

        if (waitingForTrigger) DetectTrigger();
        if (activeStackedReplay) StackedReplayUpdate();

        //Stacked replay
        bool primaryButtonValue;
        if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonValue) && primaryButtonValue)
        {
            if (firstPrimaryButtonPress)
            {
                waitingForTrigger = true;
            }
        } else if (!firstPrimaryButtonPress)
        {
            firstPrimaryButtonPress = true;
        }

        currFrame++;
        FileReplayUpdate();
    }

    void DetectTrigger()
    {
        bool triggerValue;
        if (rightHandDevices[0].TryGetFeatureValue(CommonUsages.triggerButton, out triggerValue) && triggerValue)
        {
            waitingForTrigger = false;
            StackedReplayInit();
        }
    }
    void StackedReplayInit()
    {
        PosRecordings = RecordManager.ReturnPositions();
        //Debug.Log("PosRecordings size is " + PosRecordings.Count);
        RotRecordings = RecordManager.ReturnRotations();
        firstPrimaryButtonPress = false;
        activeStackedReplay = true;
        replayFrameIndex = 0;
        shortestReplayCount = PosRecordings[0].Count;
        //for (int i = trailReplays.Count; i < PosRecordings.Count; i++)
        //{
            //Debug.Log("Recordings count: " + PosRecordings.Count);
            trailReplays.Add((GameObject)Instantiate(prefab, PosRecordings[PosRecordings.Count - 1][0], RotRecordings[RotRecordings.Count - 1][0]));
        // if the latest recording's frame count is lower than the previous lowest count, then replace the lowest count with it
            if (PosRecordings[PosRecordings.Count - 1].Count < shortestReplayCount) shortestReplayCount = PosRecordings[PosRecordings.Count - 1].Count;
       // }
        Debug.Log("Recordings count: " + PosRecordings.Count);
        Debug.Log("Trail replay count: " + trailReplays.Count);
        RecordManager.InitiateRecording();
    }

    void StackedReplayUpdate()
    {
        if (replayFrameIndex == shortestReplayCount)
        {
            activeStackedReplay = false;
            return;
        }

        for (int i = 0; i < trailReplays.Count; i++)
        {
            //Debug.Log("Curr pos: " + PosRecordings[i][replayFrameIndex]);
            //Debug.Log("Curr rot: " + RotRecordings[i][replayFrameIndex]);
            //Debug.Log("i: " + i);
            trailReplays[i].transform.position = PosRecordings[i][replayFrameIndex];
            trailReplays[i].transform.rotation = RotRecordings[i][replayFrameIndex];
        }

        replayFrameIndex++;
    }

    void FileReplayInit()
    {
        SaveLoad.LoadFile("BadGuy1.txt");
        SaveLoad.LoadFile2("BadGuy2.txt");
        SaveLoad.LoadFile3("BadGuy3.txt");

        firstLoadedObject = (GameObject)Instantiate(fileImportPrefab, Vector3.zero, Quaternion.identity);
        secondLoadedObject = (GameObject)Instantiate(fileImportPrefab, Vector3.zero, Quaternion.identity);
        thirdLoadedObject = (GameObject)Instantiate(fileImportPrefab, Vector3.zero, Quaternion.identity);

        // trailReplays.Add(newObject);
    }

    void FileReplayUpdate()
    {
        firstLoadedObject.transform.position = SaveLoad.loadedFilePositions[currFrame];
        firstLoadedObject.transform.rotation = SaveLoad.loadedFileRotations[currFrame];
        // firstLoadedObject.transform.JOYSTICKS? = SaveLoad.loadedFilePositions[currFrame];

        secondLoadedObject.transform.position = SaveLoad.loadedFilePositions2[currFrame];
        secondLoadedObject.transform.rotation = SaveLoad.loadedFileRotations2[currFrame];
        // firstLoadedObject.transform.JOYSTICKS? = SaveLoad.loadedFilePositions[currFrame];

        thirdLoadedObject.transform.position = SaveLoad.loadedFilePositions3[currFrame];
        thirdLoadedObject.transform.rotation = SaveLoad.loadedFileRotations3[currFrame];
        // firstLoadedObject.transform.JOYSTICKS? = SaveLoad.loadedFilePositions[currFrame];
    }
}
