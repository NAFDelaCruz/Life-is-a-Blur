using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialTutorial : Tutorial
{
    public List<string> InitialDialogue;
    public DialogueManager DialogueManagerScript;

    private void Start()
    {
        DialogueManagerScript.Dialogues = InitialDialogue;
        DialogueManagerScript.StartDialogue();
    }

    public override Tutorial TutorialActions()
    {
        if ((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) && TutorialIndex == 0 && DialogueManagerScript.isDialogueDone)
        {
            //TutorialUI.GetComponent<SpriteRenderer>().sprite = TutorialPrompts[TutorialIndex];
            TutorialIndex++;
            Debug.Log("Looked Around");
        }

        if ((Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") > 0f) && TutorialIndex == 1)
        {
            //TutorialUI.GetComponent<SpriteRenderer>().sprite = TutorialPrompts[TutorialIndex];
            TutorialIndex++;
            Debug.Log("Moved");
        }

        return this;    
    }
}
