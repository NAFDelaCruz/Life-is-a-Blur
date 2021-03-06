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
    bool isDialogueStarted = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        TeacherAnimator.SetBool("IsPresenting", true);
        GetGameManagerComponents();
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            PlayerRb.constraints = RigidbodyConstraints.FreezePosition | ~RigidbodyConstraints.FreezeRotationY;
            DialogueManagerScript.Dialogues = Dialogue;
            DialogueManagerScript.StartDialogue();
        }

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
            
            if (Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") > 0) StartCoroutine(Delay());
        }

        return this;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isTutorialDone = true;
    }
}
