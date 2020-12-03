using UnityEngine;
using System.Collections;

public class PauseMenuCanvasTransition : MonoBehaviour
{
    public CanvasGroup canvas_Pause;
    public CanvasGroup canvas_Options;
    public GameObject background;
    const float transitionTime = 0.2f;


    void Awake()
    {
        CanvasGroupUtil.InstantHide(canvas_Pause);
        CanvasGroupUtil.InstantHide(canvas_Options);
        background.SetActive(false);
    }

    public void Pause()
    {
        CanvasGroupUtil.InstantReveal(canvas_Pause);
        background.SetActive(true);
    }

    public void UnPause()
    {
        CanvasGroupUtil.InstantHide(canvas_Pause);
        background.SetActive(false);
    }

    public void PauseMainToOptions()
    {
        CanvasGroupUtil.InstantReveal(canvas_Options);
        CanvasGroupUtil.InstantHide(canvas_Pause);
    }

    public void OptionsToPauseMain()
    {
        CanvasGroupUtil.InstantReveal(canvas_Pause);
        CanvasGroupUtil.InstantHide(canvas_Options);
    }
}