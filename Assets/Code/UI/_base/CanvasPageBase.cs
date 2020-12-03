using UnityEngine;
using System.Collections;

public abstract class CanvasPageBase : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    protected bool isOpen;

    public virtual void SetIsOpen(bool isOpen)
    {
        if (isOpen)
        {
            CanvasGroupUtil.InstantReveal(canvasGroup);
        }
        else
        {
            CanvasGroupUtil.InstantHide(canvasGroup);
        }

        this.isOpen = isOpen;
    }

    public void ToggleOpen ()
    {
        SetIsOpen(isOpen = !isOpen);
    }
}