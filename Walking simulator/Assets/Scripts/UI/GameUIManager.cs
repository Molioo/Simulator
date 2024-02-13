using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    public bool AnyWindowOpened 
    {  
        get
        {
            return windowsOpened.Count > 0;
        } 
    }


    [SerializeField]
    private ReactionTextController _reactionTextController;

    [SerializeField]
    private float _reactionTime = 3f;

    [SerializeField]
    private CanvasGroup _canvasGroup = null;

    private List<string> windowsOpened = new List<string>();

    public void SwitchAllUIVisibility(bool visible)
    {
        _canvasGroup.alpha = visible ? 1 : 0;
    }
    
    public void ShowReactionText(string text)
    {
        _reactionTextController.SetReactionText(text, _reactionTime);
    }

    public void WindowOpened(string name)
    {
        if (windowsOpened.Contains(name) == false)
        {
            windowsOpened.Add(name);
        }

        Time.timeScale = 0.001f;
    }

    public void WindowClosed(string name)
    {
        if (windowsOpened.Contains(name))
        {
            windowsOpened.Remove(name);
        }

        if (windowsOpened.Count == 0)
        {
            Time.timeScale = 1f;
        }
    }

  
}
