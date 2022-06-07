using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTutorial : Tutorial
{
    public GameObject Player;
    public List<string> Dialogue;
    CanvasGroup CurrentTutorial;
    bool isDialogueStarted = false;
    bool isOphthalInteracted = false;
    bool hasMoved = false;

    private void Start()
    {
        GetGameManagerComponents();
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
        gameObject.AddComponent<Outline>().color = 0;
    }

    public override Tutorial TutorialActions()
    {
        if (!isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);

        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < 2f)
            if (Input.GetMouseButtonDown(1))
            {
                isOphthalInteracted = true;
                Destroy(gameObject.GetComponent<Outline>());
            }

            if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f) hasMoved = true;


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

        if (hasMoved && isOphthalInteracted) StartCoroutine(Delay());

        return this;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(3f);
        isTutorialDone = true;
    }
}
 