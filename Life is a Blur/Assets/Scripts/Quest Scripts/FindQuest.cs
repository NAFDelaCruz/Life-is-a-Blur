using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindQuest : Quest
{
    public PlayerInteraction PlayerInteractionScript;
    
    private void Awake()
    {
        QuestObject.AddComponent<Outline>().color = 0;
    }

    public override Quest QuestActions()
    { 
        if (Input.GetMouseButtonDown(0) && PlayerInteractionScript.ObjectBehavior)
        {
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.StartDialogue();
            if (NextQuest) QuestManagerScript.CurrentQuest = NextQuest;
        }

        return this;
    }
}
