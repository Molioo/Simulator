using System.Collections.Generic;
using UnityEngine;

public class SaveableAssetsReferenceList : Singleton<SaveableAssetsReferenceList>
{
    public List<ScriptableObject> SaveableAssets = new List<ScriptableObject>();
    
    public List<ISaveable> GetSaveableAssets()
    {
        List<ISaveable> saveablesFromList = new List<ISaveable>();
        foreach (ScriptableObject go in SaveableAssets)
        {
            ISaveable saveable = go as ISaveable;
            if(saveable != null)
            {
                saveablesFromList.Add(saveable);
            }
        }
        return saveablesFromList;
    }

    public void Update()
    {
        //Debug
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SaveSystem.SaveData();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SaveSystem.LoadData();
        }
    }
}
