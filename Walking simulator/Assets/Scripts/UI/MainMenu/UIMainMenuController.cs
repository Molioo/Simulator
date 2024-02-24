using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _continueButtonRect;

    private void Start()
    {
        GameManager.SwitchCursorVisible(true);
        _continueButtonRect.gameObject.SetActive(SaveSystem.HasSave());
    }
   
    public void OnNewGameButtonClicked()
    {
        GameManager.SwitchCursorVisible(false);
        LoadingManager.Instance.LoadGameScene(false);
    }

    public void OnContinueButtonClicked()
    {
        GameManager.SwitchCursorVisible(false);
        LoadingManager.Instance.LoadGameScene(true);
    }

    public void OnSettingsButtonClicked()
    {

    }

    public void OnExitButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        return;
#endif

        Application.Quit();
    }
}
