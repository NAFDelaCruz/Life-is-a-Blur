using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SquintTutorial : Tutorial
{
    public GameObject PlayerHead;
    public GameObject Blink1;
    public GameObject Blink2;
    public Animator EmilAnimator;
    public PlayableDirector EmilCutscene;
    public PlayerBlink PlayerBlinkScript;
    public PlayerMovement PlayerMovementScript;

    CanvasGroup CurrentTutorial;
    bool isDialogueStarted = false;
    bool isCutsceneStarted = false;
    bool isObjectHighlighted = false;

    private void Start()
    {
        EmilAnimator.SetTrigger("IsSitting");
        GetGameManagerComponents();
        SetValues(DialogueElements);
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (!isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);
        if (isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha -= 0.1f);

        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            SetDialogueValues();
            DialogueManagerScript.StartDialogue();
            PlayerBlinkScript.enabled = true;
        }

        if (!DialogueManagerScript.isDialogueDone && isCutsceneStarted && !isObjectHighlighted)
        {
            isObjectHighlighted = true;
            gameObject.AddComponent<Outline>().color = 0;
            StartCoroutine(DelayHead());
        }

        if (DialogueManagerScript.isDialogueDone && isCutsceneStarted && isObjectHighlighted)
        {
            PlayerBlinkScript.enabled = true;
            PlayerMovementScript.enabled = true;
            Destroy(gameObject.GetComponent<Outline>());
            Blink1.SetActive(true);
            Blink2.SetActive(true);
            StartCoroutine(TutorialDelay(NextTutorial));
        }

        if (Input.GetKey(KeyCode.B) && DialogueManagerScript.isDialogueDone && !isCutsceneStarted)
        {
            isCutsceneStarted = true;
            StartCoroutine(Delay());
        }

        return this;
    }

    IEnumerator DelayHead()
    {
        yield return new WaitForSeconds(1f);
        PlayerHead.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        PlayerMovementScript.xRotation = 0f;
        PlayerMovementScript.yRotation = 0f;
        Blink1.SetActive(false);
        Blink2.SetActive(false);
        isTutorialDone = true;
        PlayerBlinkScript.enabled = false;
        PlayerMovementScript.enabled = false;
        EmilCutscene.Play();
        SetValues(DialogueElementsExtra1);
        SetDialogueValues();
        DialogueManagerScript.StartDialogue();
    }
}
