using UnityEngine;
using System.Collections;

public abstract class CanvasPageToggle : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    protected bool isOpen;

    public virtual void ToggleOpen()
    {
        SetIsOpen(isOpen = !isOpen);
    }

    public virtual void SetIsOpen(bool isOpen)
    {
        this.isOpen = isOpen;
        if (isOpen)
        {
            CanvasGroupUtil.InstantReveal(canvasGroup);
        }
        else
        {
            CanvasGroupUtil.InstantHide(canvasGroup);
        }
    }
}