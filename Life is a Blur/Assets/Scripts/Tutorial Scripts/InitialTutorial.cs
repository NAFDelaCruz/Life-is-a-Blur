using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialTutorial : Tutorial
{
    public List<string> InitialDialogue;
    public DialogueManager DialogueManagerScript;
    PlayerMovement PlayerMovementScript;

    private void Start()
    {
        PlayerMovementScript = GameObject.Find("Player Body").GetComponent<PlayerMovement>();
        PlayerMovementScript.enabled = false;
        DialogueManagerScript.Dialogues = InitialDialogue;
        DialogueManagerScript.StartDialogue();
    }

    public override Tutorial TutorialActions()
    {
        if (DialogueManagerScript.isDialogueDone)
        {
            PlayerMovementScript.enabled = true;
            TutorialUI.SetActive(true);

            if ((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) && TutorialIndex == 0 && isTutorialDone) IncrementTutorial();

            if (Input.GetKey(KeyCode.B) && TutorialIndex == 1 && isTutorialDone) IncrementTutorial();

            if ((Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") > 0f) && TutorialIndex == 2 && isTutorialDone) IncrementTutorial();


            if (TutorialIndex == 3 && isTutorialDone)
            {
                TutorialUI.SetActive(false);
            }
        }

        return this;    
    }

    void IncrementTutorial()
    {
        TutorialIndex++;
        StartCoroutine(TutorialDelay(TutorialPrompts[TutorialIndex - 1], TutorialPrompts[TutorialIndex]));
    }
}
