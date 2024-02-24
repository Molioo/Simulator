using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : Singleton<LoadingManager>
{
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameScene(bool loadSaveData)
    {
        StartCoroutine(LoadGameSceneRoutine(loadSaveData));
    }

    private IEnumerator LoadGameSceneRoutine(bool loadData)
    {
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

        if(loadData)
        {
            SaveSystem.LoadData();
        }

        yield return UiLoadingPanelController.Instance.FadeOut();
    }
}
