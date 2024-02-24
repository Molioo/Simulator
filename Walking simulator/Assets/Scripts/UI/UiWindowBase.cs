using UnityEngine;

public class UiWindowBase : MonoBehaviour
{
    protected bool IsShown = false;

    public virtual void Show()
    {
        GameUIManager.Instance.WindowOpened(name);
        IsShown = true;
    }

    public virtual void Hide()
    {
        GameUIManager.Instance.WindowClosed(name);
        IsShown = false;
    }
}
