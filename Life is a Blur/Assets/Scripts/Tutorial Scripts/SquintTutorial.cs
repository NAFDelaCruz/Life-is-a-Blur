using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SquintTutorial : Tutorial
{
    public GameObject PlayerHead;
    public Animator EmilAnimator;
    public PlayableDirector EmilCutscene;
    public PlayerBlink PlayerBlinkScript;
    public PlayerMovement PlayerMovementScript;
    public List<string> Dialogue1;
    public List<string> Dialogue2;

    CanvasGroup CurrentTutorial;
    bool isDialogueStarted = false;
    bool isCutsceneStarted = false;
    bool isObjectHighlighted = false;

    private void Start()
    {
        EmilAnimator.SetTrigger("IsSitting");
        GetGameManagerComponents();
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (!isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);
        if (isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha -= 0.1f);

        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            DialogueManagerScript.Dialogues = Dialogue1;
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
        }

        if (Input.GetKey(KeyCode.B) && DialogueManagerScript.isDialogueDone && !isCutsceneStarted)
        {
            isCutsceneStarted = true;
            StartCoroutine(Delay());
        }

        return this;
    }

    public void Done()
    {
        StartCoroutine(TutorialDelay(NextTutorial));
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
        isTutorialDone = true;
        PlayerBlinkScript.enabled = false;
        PlayerMovementScript.enabled = false;
        EmilCutscene.Play();
        DialogueManagerScript.Dialogues = Dialogue2;
        DialogueManagerScript.StartDialogue();
    }
}
