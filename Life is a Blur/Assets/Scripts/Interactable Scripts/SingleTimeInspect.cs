using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTimeInspect : InteractableObject
{
    public DialogueManager DialogueManagerScript;

    public override InteractableObject Interact()
    {
        DialogueManagerScript.Dialogues = InspectDialogue;
        DialogueManagerScript.StartDialogue();

        return this;
    }
}
