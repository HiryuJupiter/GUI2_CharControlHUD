using UnityEngine;
using System.Collections;

public class CharacterCretionColorizer : MonoBehaviour
{
    public Color[] colors;
    public MeshRenderer hair;
    public MeshRenderer eyeL;
    public MeshRenderer eyeR;
    public MeshRenderer head;
    public MeshRenderer body;

    int hairIndex, eyeIndex, headIndex, bodyIndex;

    int colorCount;

    void Start()
    {
        colorCount = colors.Length;
        hairIndex = GetRandomIndex();
        eyeIndex = GetRandomIndex();
        headIndex = GetRandomIndex();
        bodyIndex = GetRandomIndex();
        ColorHair();
        ColorEyes();
        ColorHead();
        ColorBody();
    }

    public void ColorHair (int modification = 0)
    {
        hairIndex = GetClampedIndex(hairIndex + modification);
        hair.material.color = colors[hairIndex];
    }

    public void ColorEyes(int modification = 0)
    {
        eyeIndex = GetClampedIndex(eyeIndex + modification);

        eyeL.material.color = colors[eyeIndex];
        eyeR.material.color = colors[eyeIndex];
    }

    public void ColorHead(int modification = 0)
    {
        headIndex = GetClampedIndex(headIndex + modification);
        head.material.color = colors[headIndex];
    }
    public void ColorBody(int modification = 0)
    {
        bodyIndex = GetClampedIndex(bodyIndex + modification);
        body.material.color = colors[bodyIndex];
    }

    int GetClampedIndex (int index)
    {
        if (index < 0)
            index = colorCount - 1;
        else if (index >= colorCount)
            index = 0;
        return index;
    }

    int GetRandomIndex ()
    {
        return Random.Range(0, colorCount);
    }
}