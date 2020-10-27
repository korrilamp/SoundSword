 using UnityEngine;
 using System.Collections;
 using System.Collections.Generic;
 using System.Runtime.Serialization.Formatters.Binary;
 using System.IO;
 
 public static class SaveLoad
 {

    public static List<Vector3> loadedFilePositions = new List<Vector3>();
    public static List<Quaternion> loadedFileRotations = new List<Quaternion>();
    public static List<Vector2> loadedFileJoysticks = new List<Vector2>();


    public static List<Vector3> loadedFilePositions2 = new List<Vector3>();
    public static List<Quaternion> loadedFileRotations2 = new List<Quaternion>();
    public static List<Vector2> loadedFileJoysticks2 = new List<Vector2>();


    public static List<Vector3> loadedFilePositions3 = new List<Vector3>();
    public static List<Quaternion> loadedFileRotations3 = new List<Quaternion>();
    public static List<Vector2> loadedFileJoysticks3 = new List<Vector2>();
    
    private static void Awake() {
        
    }

    private static void Update() {
        
    }
    
    public static void Save(List<Vector3> positions, List<Quaternion> rotations, List<Vector2> joysticks) {
        SaveObject saveObject = new SaveObject {
            positions = positions,
            rotations = rotations,
            joysticks = joysticks,
        };

        string json = JsonUtility.ToJson(saveObject);

        Debug.Log("Saved!");

        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public static void Load() {
        if(File.Exists(Application.dataPath + "/save.txt")) {
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            Debug.Log("Loaded data!");

            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);

            List<Vector3> positions = loadedObject.positions;
            List<Quaternion> rotations = loadedObject.rotations;
            List<Vector2> joysticks = loadedObject.joysticks;

            // Debug.Log("Loaded positions");
            // for (int i = 0; i < positions.Count; i++) {
            //     Debug.Log(positions[i]);
            //     Debug.Log(rotations[i]);
            // }
            //Debug.Log("Average time: " + averageTime);
        }

    }

    public static void LoadFile(string filename)
    {
        if (File.Exists(Application.dataPath + "/" + filename))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/" + filename);
            Debug.Log("Loaded data!");

            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);

            loadedFilePositions = loadedObject.positions;
            loadedFileRotations = loadedObject.rotations;
            loadedFileJoysticks = loadedObject.joysticks;

            Debug.Log("Loaded file positions!");
            // for (int i = 0; i < positions.Count; i++) {
            //     Debug.Log(positions[i]);
            //     Debug.Log(rotations[i]);
            // }
            //Debug.Log("Average time: " + averageTime);
        }
    }

    public static void LoadFile2(string filename)
    {
        if (File.Exists(Application.dataPath + "/" + filename))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/" + filename);
            Debug.Log("Loaded data!");

            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);

            loadedFilePositions2 = loadedObject.positions;
            loadedFileRotations2 = loadedObject.rotations;
            loadedFileJoysticks2 = loadedObject.joysticks;

            Debug.Log("Loaded file positions!");
            // for (int i = 0; i < positions.Count; i++) {
            //     Debug.Log(positions[i]);
            //     Debug.Log(rotations[i]);
            // }
            //Debug.Log("Average time: " + averageTime);
        }
    }

    public static void LoadFile3(string filename)
    {
        if (File.Exists(Application.dataPath + "/" + filename))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/" + filename);
            Debug.Log("Loaded data!");

            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(saveString);

            loadedFilePositions3 = loadedObject.positions;
            loadedFileRotations3 = loadedObject.rotations;
            loadedFileJoysticks3 = loadedObject.joysticks;

            Debug.Log("Loaded file positions!");
            // for (int i = 0; i < positions.Count; i++) {
            //     Debug.Log(positions[i]);
            //     Debug.Log(rotations[i]);
            // }
            //Debug.Log("Average time: " + averageTime);
        }
    }

    public class SaveObject {
        public List<Vector3> positions = new List<Vector3>();
        public List<Quaternion> rotations = new List<Quaternion>();
        public List<Vector2> joysticks = new List<Vector2>();
    }
}