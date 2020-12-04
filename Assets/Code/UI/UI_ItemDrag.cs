using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_ItemDrag : MonoBehaviour
{
    public static UI_ItemDrag Instance { get; private set; }

    Item item;
    Image image;
    RectTransform trans;
    bool showing;

    void Awake()
    {
        Instance = this;

        trans = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (showing)
        {
            trans.position = Input.mousePosition;
        }
    }

    public void ShowItem (Item item)
    {
        this.item = item;
        showing = true;
        image.enabled = true;
        image.sprite = ItemDirectory.GetItem(item.ID).icon;
    }

    public void ClearAndHide ()
    {
        item = null;
        showing = false;
        image.enabled = false;
    }

    void OnMouseRelease ()
    {
        ClearAndHide();
    }
}