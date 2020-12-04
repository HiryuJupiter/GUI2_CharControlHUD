using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeadScreen : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    protected bool isOpen;

    public void ToggleOpen()
    {
        SetIsOpen(isOpen = !isOpen);
    }
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
}