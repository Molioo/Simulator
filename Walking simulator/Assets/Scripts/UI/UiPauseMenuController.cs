using UnityEngine;

public class UiPauseMenu : UiWindowBase
{
    [SerializeField]
    private CanvasGroup _canvasGroup;

    public void OnContinueClicked()
    {
       SwitchPauseMenu();
    }

    public void OnGoToMenuClicked()
    {
        SwitchPauseMenu();
        SaveSystem.SaveData();
        LoadingManager.Instance?.LoadMenuScene();
    }

    public void SwitchPauseMenu()
    {
        if (IsShown)
        {
            Hide();
            _canvasGroup.SwitchAllValues(false);
        }
        else
        {
            Show();
            _canvasGroup.SwitchAllValues(true);
        }
    }
    
}
