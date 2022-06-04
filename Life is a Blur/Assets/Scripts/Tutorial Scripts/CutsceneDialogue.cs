using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneDialogue : Tutorial
{
    public List<string> InitialDialogue;

    private void Start()
    {
        GetGameManagerComponents();
        DialogueManagerScript.Dialogues = InitialDialogue;
        DialogueManagerScript.StartDialogue();
    }

    public override Tutorial TutorialActions()
    {
        throw new System.NotImplementedException();
    }
}
