using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Molioo.Simulator.Quests
{

    //public class QuestsSaveSystem : MonoBehaviour
    //{
    //    private const string SAVE_FILE_NAME = "QuestsData.json";

    //    private const string SETTINGS_FILE_NAME = "Settings.json";


    //    public static string SaveFilePath { get { return Application.persistentDataPath + "/" + SAVE_FILE_NAME; } }

    //    public static string SettingsFilePath { get { return Application.persistentDataPath + "/" + SETTINGS_FILE_NAME; } }

    //    public static GameData CurrentGameData;

    //    public static void SaveProgress(List<Quest> allQuests)
    //    {
    //        StreamWriter file = new StreamWriter(SaveFilePath, false);
    //        file.Write(JsonUtility.ToJson(CurrentGameData));
    //        file.Close();
    //    }

    //    public static void LoadProgress()
    //    {
    //        try
    //        {
    //            string jsonStats = File.ReadAllText(SaveFilePath);
    //            if (string.IsNullOrEmpty(jsonStats))
    //            {
    //                Debug.Log("File was empty or nulled");
    //                CurrentGameData = new GameData();
    //                CurrentGameData.PopulateNewTutorialSaves();
    //            }
    //            else
    //            {
    //                GameData gameDataFromFile = JsonUtility.FromJson<GameData>(jsonStats);
    //                CurrentGameData = gameDataFromFile;
    //                Debug.Log("Succesfully loaded tutorial saves from file" + SaveFilePath);
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            CurrentGameData = new GameData();
    //            CurrentGameData.PopulateNewTutorialSaves();
    //            Debug.Log("Something went wrong when reading file : " + e.Message);
    //            return;
    //        }
    //    }

    //    public static void SaveSettings(SettingsData settingsToSave)
    //    {
    //        StreamWriter file = new StreamWriter(SettingsFilePath, false);
    //        file.Write(JsonUtility.ToJson(settingsToSave));
    //        file.Close();
    //    }

    //    public static SettingsData LoadSettings()
    //    {
    //        try
    //        {
    //            string jsonSettings = File.ReadAllText(SettingsFilePath);
    //            if (string.IsNullOrEmpty(jsonSettings))
    //            {
    //                Debug.Log("File was empty or nulled");
    //                return new SettingsData();
    //            }
    //            else
    //            {
    //                SettingsData settingsFromFile = JsonUtility.FromJson<SettingsData>(jsonSettings);
    //                Debug.Log("Succesfully loaded settings from file");
    //                return settingsFromFile;
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Debug.Log("Something went wrong when reading file : " + e.Message);
    //            return new SettingsData();
    //        }
    //    }
    //}
}