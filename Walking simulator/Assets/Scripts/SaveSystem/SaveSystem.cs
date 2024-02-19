using Molioo.Simulator.Quests;
using Newtonsoft.Json;
using Palmmedia.ReportGenerator.Core.Common;
using System;
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
        Dictionary<string, Dictionary<string, string>> allData = new Dictionary<string, Dictionary<string, string>>();
        // Collect all the data.
        foreach (ISaveable saveable in _saveables)
        {
            if (string.IsNullOrEmpty(saveable.UniqueID))
            {
                Debug.LogError("Saveable Unique ID is null or empty");
            }
            allData.Add(saveable.UniqueID, saveable.OnSave());
        }

        Debug.Log("Saveables count " + allData.Count);
        SaveWrapper saveWrapper = new SaveWrapper(allData);
        SaveDataJson(saveWrapper);
        //Save the data.
        //SaveDataBinary(allData);
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
        //Dictionary<string, Dictionary<string, object>> allData = LoadDataBinary<Dictionary<string, Dictionary<string, object>>>();
        Debug.Log("Try to load wrapper");
        SaveWrapper wrapper = LoadDataJson();
        if (wrapper.Data == null)
        {
            Debug.LogError("Save File NOT FOUND");
            return;
        }
        else
        {
            Debug.Log("Save file data was not null");
        }

        Debug.Log("Saveables " + _saveables.Count);

        //Iterate and load onto our objects
        foreach (ISaveable saveable in _saveables)
        {
            if (wrapper.Data.ContainsKey(saveable.UniqueID))
            {
                Debug.Log("Loaded data for " + saveable.UniqueID);
                saveable.OnLoad(wrapper.Data[saveable.UniqueID]);
            }
            else
            {
                Debug.Log($"Saveable with unique id {saveable.UniqueID} is not in all data list");
            }
        }
    }

    public static void DebugData(Dictionary<string, string> objectData)
    {
        foreach (KeyValuePair<string, string> kvp in objectData)
        {
            Debug.Log($"Key: { kvp.Key.ToString()}, Value: {kvp.Value.ToString()}");
        }
    }

    public static void SaveDataJson(SaveWrapper wrapper)
    {
        StreamWriter file = new StreamWriter(SaveFilePath, false);
        //string content = JsonUtility.ToJson(wrapper);
        string content = JsonConvert.SerializeObject(wrapper, Formatting.None);
        file.Write(content);
        file.Close();
        Debug.Log("Saved current quests");
        Debug.Log(content);
    }

    public static SaveWrapper LoadDataJson()
    {
        try
        {
            string jsonContent = File.ReadAllText(SaveFilePath);
            Debug.Log("Loaded json " + jsonContent);
            if (string.IsNullOrEmpty(jsonContent))
            {
                Debug.Log("File was empty or nulled");
                return new SaveWrapper();
            }
            else
            {
                //SaveWrapper wrapper = JsonUtility.FromJson<SaveWrapper>(jsonContent);
                Debug.Log("Illl try to deserialize it");
                SaveWrapper wrapper = JsonConvert.DeserializeObject<SaveWrapper>(jsonContent);
                Debug.Log("And after, wrapper is null? " + (wrapper==null));
                return wrapper;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Something went wrong when reading file : " + e.Message);
            return new SaveWrapper();
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

    public class SaveWrapper
    {
        public Dictionary<string, Dictionary<string, string>> Data;
        public int A = 5;
        public string Name = "Watafak";

        public SaveWrapper()
        {

        }

        public SaveWrapper(Dictionary<string, Dictionary<string, string>> data)
        {
            Data = data;
            A = 10;
            Name = "Tomek";
        }
    }
}
