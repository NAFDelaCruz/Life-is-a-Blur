using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorial : Tutorial
{
    public List<string> Dialogue;
    CanvasGroup CurrentTutorial;
    bool isDialogueStarted = false;

    private void Start()
    {
        GetGameManagerComponents();
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (!isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);

        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            DialogueManagerScript.Dialogues = Dialogue;
            DialogueManagerScript.StartDialogue();
        }

        if (isTutorialDone)
        {
            CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha -= 0.1f);
            StartCoroutine(TutorialDelay(NextTutorial));
        }

        if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f) StartCoroutine(Delay());

        return this;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isTutorialDone = true;
    }
}
 