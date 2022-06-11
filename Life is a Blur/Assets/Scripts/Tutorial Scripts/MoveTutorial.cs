using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoveTutorial : Tutorial
{
    public GameObject Player;
    public PlayableDirector DoctorCutscene;
    public List<string> Dialogue;
    
    Rigidbody PlayerRb;
    CanvasGroup CurrentTutorial;
    bool isDialogueStarted = false;
    bool isOphthalInteracted = false;
    bool hasMoved = false;

    private void Start()
    {
        PlayerRb = Player.GetComponent<Rigidbody>();
        GetGameManagerComponents();
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (!isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);

        if (Vector3.Distance(gameObject.transform.position, Player.transform.position) < 5f)
            if (Input.GetMouseButtonDown(1))
            {
                isOphthalInteracted = true;
                Destroy(gameObject.GetComponent<Outline>());
            }

        if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f) hasMoved = true;

        if (isTutorialDone) CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha -= 0.1f);

        if (!isDialogueStarted)
        {
            PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
            StartCoroutine(HintDelay());
            isDialogueStarted = true;
            DialogueManagerScript.Dialogues = Dialogue;
            DialogueManagerScript.StartDialogue();
        }

        if (hasMoved && isOphthalInteracted)
        {
            DoctorCutscene.Play();
            isTutorialDone = true;
        }

        return this;
    }

    IEnumerator HintDelay()
    {
        yield return new WaitForSeconds(120f);
        gameObject.AddComponent<Outline>().color = 0;
    }
}
 