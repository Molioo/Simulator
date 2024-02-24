using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    private bool _isLoadingInProgress;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameScene(bool loadSaveData)
    {
        if (_isLoadingInProgress)
        {
            return;
        }

        StartCoroutine(LoadGameSceneRoutine(loadSaveData));
    }

    public void LoadMenuScene()
    {
        if (_isLoadingInProgress)
        {
            return;
        }

        StartCoroutine(LoadMenuSceneRoutine());
    }

    private IEnumerator LoadGameSceneRoutine(bool loadData)
    {
        _isLoadingInProgress = true;
        yield return UiLoadingPanelController.Instance.FadeIn();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        while(loadOperation.isDone == false)
        {
            yield return null;
        }

        AsyncOperation unloadMenuOperation = SceneManager.UnloadSceneAsync("MainMenu");
        while(unloadMenuOperation.isDone == false)
        {
            yield return null;
        }

        if (loadData)
        {
            SaveSystem.LoadData();
        }
        else
        {
            SaveSystem.FindSaveables();
        }

        yield return UiLoadingPanelController.Instance.FadeOut();
        _isLoadingInProgress = false;
    }

    private IEnumerator LoadMenuSceneRoutine()
    {
        _isLoadingInProgress = true;
        yield return UiLoadingPanelController.Instance.FadeIn();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        while (loadOperation.isDone == false)
        {
            yield return null;
        }

        AsyncOperation unloadMenuOperation = SceneManager.UnloadSceneAsync("GameScene");
        while (unloadMenuOperation.isDone == false)
        {
            yield return null;
        }

        SaveSystem.ClearSaveables();

        yield return UiLoadingPanelController.Instance.FadeOut();
        _isLoadingInProgress = false;
    }
}
