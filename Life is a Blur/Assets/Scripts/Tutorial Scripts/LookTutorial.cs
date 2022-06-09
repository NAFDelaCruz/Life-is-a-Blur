using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTutorial : Tutorial
{
    public List<string> Dialogue;
    public PlayerInteraction PlayerInteractionScript;
    public PlayerMovement PlayerMovementScript;
    public Animator TeacherAnimator;
    public CanvasGroup GameUI;

    CanvasGroup CurrentTutorial;

    private void Start()
    {
        TeacherAnimator.SetBool("IsPresenting", true);
        GetGameManagerComponents();
        DialogueManagerScript.Dialogues = Dialogue;
        DialogueManagerScript.StartDialogue();
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (isTutorialDone)
        {
            CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha -= 0.1f);
            if (DialogueManagerScript.isDialogueDone) StartCoroutine(TutorialDelay(NextTutorial));
        }

        if (DialogueManagerScript.isDialogueDone && !isTutorialDone)
        {
            CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);
            GameUI.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);
            PlayerMovementScript.enabled = true;


            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0) isTutorialDone = true;
        }

        return this;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isTutorialDone = true;
    }
}
