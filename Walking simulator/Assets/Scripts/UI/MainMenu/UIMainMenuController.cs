using UnityEngine;

public class UIMainMenuController : MonoBehaviour
{
    [SerializeField]
    private RectTransform _continueButtonRect;

    private void Start()
    {
        _continueButtonRect.gameObject.SetActive(SaveSystem.HasSave());
    }
   
    public void OnNewGameButtonClicked()
    {
        LoadingManager.Instance.LoadGameScene(false);
    }

    public void OnContinueButtonClicked()
    {
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
