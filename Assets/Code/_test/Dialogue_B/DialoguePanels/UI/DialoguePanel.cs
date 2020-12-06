using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanel : MonoBehaviour
{

    public Image portraitImage;
    public Image panelBG;
    public Text nameLabel;
    public Text dialogueBox;

    bool animatingText;
    string previousDialogueText;
    public void GreyOutPanel ()
    {
        SetCharacterMask(true);
        StopAnimatingText();
    }

    public void DisplayDialogue(Dialogue dialogue)
    {
        SetCharacterMask(false);

        //Display dialogue character's name, portrait image, and dialogue text
        nameLabel.text = dialogue.name;
        portraitImage.sprite = SpriteAtlasLoader.instance.LoadSprite(dialogue.atlasImageName);

        if (animatingText)
        {
            StopAnimatingText();
        }

        previousDialogueText = dialogue.dialogueText;
        StartCoroutine(AnimateText(previousDialogueText));
    }

    public void Clear ()
    {
        dialogueBox.text = "";
    }

    void StopAnimatingText ()
    {
        if (animatingText)
        {
            StopAllCoroutines();
            dialogueBox.text = previousDialogueText;
            animatingText = false;
        }
    }

    void SetCharacterMask(bool reveal)
    {
        if (reveal)
        {
            portraitImage.color = Color.grey;
            panelBG.color = Color.grey;
        }
        else
        {
            portraitImage.color = Color.white;
            panelBG.color = Color.white;
        }
    }

    IEnumerator AnimateText (string dialogueText)
    {
        animatingText = true;
        dialogueBox.text = "";

        foreach (char letter in dialogueText)
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        animatingText = false;
    }
}