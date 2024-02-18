using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    private const string SAVE_FILE_NAME = "Save.json";
    public static string SaveFilePath { get { return Application.persistentDataPath + "/" + SAVE_FILE_NAME; } }

    private static List<ISaveable> _saveables = new List<ISaveable>();

    private static BinaryFormatter formatter;


    public static void FindSaveObjects()
    {
        //_saveObjects = new List<GameObject>();
        _saveables = new List<ISaveable>();

        // FInd all objects, even in a large scene this may only take a second or 2.
        GameObject[] gos = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        // Iterate the objects.
        foreach (GameObject go in gos)
        {
            if (go.TryGetComponent(out ISaveable saveable))
            {
                //_saveObjects.Add(go);
                _saveables.Add(saveable);
            }
        }

        _saveables.AddRange(SaveableAssetsReferenceList.Instance.GetSaveableAssets());
    }

    public static bool HasSave()
    {
        return File.Exists(SaveFilePath);
    }

    public static void SaveData()
    {
        //Check if initialized
        if (_saveables == null || _saveables.Count == 0)
            FindSaveObjects();

        // Create our data object
        Dictionary<string, Dictionary<string, object>> allData = new Dictionary<string, Dictionary<string, object>>();
        // Collect all the data.
        foreach (ISaveable saveable in _saveables)
        {
            if (string.IsNullOrEmpty(saveable.UniqueID))
            {
                Debug.LogError("Saveable Unique ID is null or empty");
            }
            allData.Add(saveable.UniqueID, saveable.OnSave());
        }
        //Save the data.
        SaveDataBinary(allData);
    }

    /// <summary>
    /// Load the data stucture from the file system.
    /// </summary>
    public static void LoadData()
    {
        //Check if we have initialized
        if (_saveables == null || _saveables.Count == 0)
            FindSaveObjects();

        //Get our data
        Dictionary<string, Dictionary<string, object>> allData = LoadDataBinary<Dictionary<string, Dictionary<string, object>>>();
        if (allData == null)
        {
            Debug.LogWarning("Save File NOT FOUND");
            return;
        }

        //Iterate and load onto our objects
        foreach (ISaveable saveable in _saveables)
        {
            if (allData.ContainsKey(saveable.UniqueID))
            {
                saveable.OnLoad(allData[saveable.UniqueID]);
            }
            else
            {
                Debug.Log($"Saveable with unique id {saveable.UniqueID} is not in all data list");
            }
        }
    }

    public static void DebugData(Dictionary<string, object> objectData)
    {
        foreach (KeyValuePair<string, object> kvp in objectData)
        {
            Debug.Log($"Key: { kvp.Key.ToString()}, Value: {kvp.Value.ToString()}");
        }
    }


    private static void SaveDataBinary(object obj)
    {
        formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);
        formatter.Serialize(fileStream, obj);
        fileStream.Close();
    }

    private static T LoadDataBinary<T>()
    {
        object obj = null;
        formatter = new BinaryFormatter();
        if (File.Exists(SaveFilePath))
        {
            FileStream fileStream = new FileStream(SaveFilePath, FileMode.Open, FileAccess.Read);
            obj = formatter.Deserialize(fileStream);
            fileStream.Close();
        }
        return (T)obj;
    }

    [SerializeField]
    private class SaveWrapper
    {
        public Dictionary<string, Dictionary<string, object>> Data;

        public SaveWrapper(Dictionary<string, Dictionary<string, object>> data)
        {
            Data = data;
        }
    }
}
