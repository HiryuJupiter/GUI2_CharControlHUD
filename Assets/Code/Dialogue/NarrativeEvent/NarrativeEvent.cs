using System.Collections.Generic;

public class NarrativeEvent
{
    public List<Dialogue> dialogues;
}

public struct Dialogue
{
    public int position;
    public string name;
    public string atlasImageName;
    public string dialogueText;
}