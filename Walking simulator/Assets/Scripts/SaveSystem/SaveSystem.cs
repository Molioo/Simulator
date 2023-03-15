using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    private const string SAVE_FILE_NAME = "Save.json";
    public static string SaveFilePath { get { return Application.persistentDataPath + "/" + SAVE_FILE_NAME; } }

    private static List<GameObject> _saveObjects;

    //private static List<ISaveable> _saveables = new List<ISaveable>();

    private static BinaryFormatter formatter;


    public static void FindSaveObjects()
    {
        _saveObjects = new List<GameObject>();
        // FInd all objects, even in a large scene this may only take a second or 2.
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
        // Iterate the objects.
        foreach (GameObject go in gos)
        {
            if(go.TryGetComponent(out ISaveable saveable))
            {
                _saveObjects.Add(go);
            }
        }
    }


    public static void SaveData()
    {
        //Check if initialized
        if (_saveObjects == null || _saveObjects.Count == 0)
            FindSaveObjects();

        // Create our data object
        Dictionary<string, Dictionary<string, object>> allData = new Dictionary<string, Dictionary<string, object>>();
        // Collect all the data.
        foreach (GameObject go in _saveObjects)
        {
            ISaveable isave = go.GetComponent<ISaveable>();
            Debug.Log("Adding to alldata");
            allData.Add(isave.UniqueID, isave.OnSave());
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
        if (_saveObjects == null || _saveObjects.Count == 0)
            FindSaveObjects();

        //Get our data
        Dictionary<string, Dictionary<string, object>> allData = LoadDataBinary<Dictionary<string, Dictionary<string, object>>>();
        if (allData == null)
        {
            Debug.LogWarning("Save File NOT FOUND");
            return;
        }
        //Iterate and load onto our objects
        foreach (GameObject go in _saveObjects)
        {
            ISaveable isave = go.GetComponent<ISaveable>();
            isave.OnLoad(allData[isave.UniqueID]);
        }
    }


    public static void SaveDataBinary(object obj)
    {
        formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(SaveFilePath, FileMode.Create, FileAccess.Write);
        formatter.Serialize(fileStream, obj);
        fileStream.Close();
    }

    public static T LoadDataBinary<T>()
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
