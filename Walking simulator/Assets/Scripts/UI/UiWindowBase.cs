using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiWindowBase : MonoBehaviour
{
    public virtual void Show()
    {
        GameUIManager.Instance.WindowOpened(name);
    }

    public virtual void Hide()
    {
        GameUIManager.Instance.WindowClosed(name);
    }
}
