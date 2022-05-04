using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialTutorial : Tutorial
{
    public List<string> InitialDialogue;
    public DialogueManager DialogueManagerScript;
    public bool toSquint;
    PlayerMovement PlayerMovementScript;

    private void Start()
    {
        PlayerMovementScript = GameObject.Find("Player Body").GetComponent<PlayerMovement>();
        //PlayerMovementScript.enabled = false;
        //DialogueManagerScript.Dialogues = InitialDialogue;
        //DialogueManagerScript.StartDialogue();
    }

    public override Tutorial TutorialActions()
    {
        if (DialogueManagerScript.isDialogueDone)
        {
            PlayerMovementScript.enabled = true;

            if ((Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) && TutorialIndex == 0 && isTutorialDone)
            {
                //TutorialUI.GetComponent<Image>().sprite = TutorialPrompts[TutorialIndex];
                Debug.Log("Looking");
                TutorialIndex++;
                StartCoroutine(TutorialDelay());
            }

            if ((Input.GetAxis("Vertical") > 0f || Input.GetAxis("Horizontal") > 0f) && TutorialIndex == 1 && isTutorialDone)
            {
                //TutorialUI.GetComponent<Image>().sprite = TutorialPrompts[TutorialIndex];
                TutorialIndex++;
                Debug.Log("Moving");
                StartCoroutine(TutorialDelay());
            }

            if (toSquint && Input.GetKey(KeyCode.B) && TutorialIndex == 2 && isTutorialDone)
            {
                //TutorialUI.GetComponent<Image>().sprite = TutorialPrompts[TutorialIndex];
                Debug.Log("Squinting");
                StartCoroutine(TutorialDelay());
            }
        }

        return this;    
    }
}
