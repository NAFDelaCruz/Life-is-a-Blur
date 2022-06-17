using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoveTutorial : Tutorial
{
    public List<string> Dialogue;
    public Rigidbody PlayerRb;
    public QuestManager QuestManagerScript;

    CanvasGroup CurrentTutorial;
    bool isDialogueStarted = false;
    bool hasMoved = false;

    private void Start()
    {
        GetGameManagerComponents();
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            DialogueManagerScript.Dialogues = Dialogue;
            DialogueManagerScript.StartDialogue();
            gameObject.AddComponent<Outline>().color = 0;
            PlayerRb.constraints = RigidbodyConstraints.FreezePosition | ~RigidbodyConstraints.FreezeRotationY;
        }

        if (!isTutorialDone && isDialogueStarted && DialogueManagerScript.isDialogueDone)
        {
            PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
            CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);
        }

        if ((Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f) && DialogueManagerScript.isDialogueDone) hasMoved = true;

        if (isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha -= 0.1f);

        if (hasMoved) StartCoroutine(Delay());

        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isTutorialDone)
        {
            QuestManagerScript.enabled = true;
            PlayerRb.constraints = RigidbodyConstraints.FreezePosition | ~RigidbodyConstraints.FreezeRotationY;
            Destroy(gameObject.GetComponent<Outline>());
            StartCoroutine(TutorialDelay(NextTutorial));
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isTutorialDone = true;
    }
}
 