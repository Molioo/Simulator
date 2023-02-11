using System.IO;
using System;
using UnityEngine;
using System.Collections.Generic;

namespace Molioo.Simulator.Quests
{

    public class QuestsSaveSystem : MonoBehaviour
    {
        private const string SAVE_FILE_NAME = "QuestsData.json";

        public static string SaveFilePath { get { return Application.persistentDataPath + "/" + SAVE_FILE_NAME; } }

        public static void SaveQuestsProgress(List<QuestData> currentQuests)
        {
            StreamWriter file = new StreamWriter(SaveFilePath, false);
            string content = JsonUtility.ToJson(new QuestsWrapper(currentQuests));
            file.Write(content);
            file.Close();
            Debug.Log("Saved current quests");
            Debug.Log(content);
        }

        public static List<QuestData> LoadCurrentQuests()
        {
            try
            {
                string jsonContent = File.ReadAllText(SaveFilePath);
                if (string.IsNullOrEmpty(jsonContent))
                {
                    Debug.Log("File was empty or nulled");
                    return new List<QuestData>();
                }
                else
                {
                    QuestsWrapper wrapper = JsonUtility.FromJson<QuestsWrapper>(jsonContent);
                    return wrapper.CurrentQuests;
                }
            }
            catch (Exception e)
            {
                Debug.Log("Something went wrong when reading file : " + e.Message);
                return new List<QuestData>();
            }
        }

    }
}