using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public EPlayerGameMode CurrentPlayerGameMode;

    public void SwitchCursorVisible(bool visible)
    {
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = visible;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveSystem.SaveData();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SaveSystem.LoadData();
        }
    }

}
