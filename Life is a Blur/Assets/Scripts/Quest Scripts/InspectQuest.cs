using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectQuest : Quest
{
    public PlayerInteraction PlayerInteractionScript;
    public PlayerMovement PlayerMovementScript;
    public bool isCutSceneNeeded;

    InspectToAllowCutscene InspectToAllowCutsceneObject;
    bool isDialogueStarted;
    bool isQuestDone;

    private void Start()
    {
        GetGameManagerComponents();
        SetValues(DialogueElements);
    }

    public override Quest QuestActions()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            SetDialogueValues();
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone)
        {
            if (!PlayerMovementScript.enabled) StartCoroutine(PromptDelay());
            PlayerMovementScript.enabled = true;



            if (isQuestDone && NextQuest)
            {
                if (isCutSceneNeeded) InspectToAllowCutsceneObject.isInteractable = true;
                StartCoroutine(NextQuestDelay(NextQuest));
            }
        }

        if (Input.GetMouseButtonDown(1) && PlayerInteractionScript.InteractableObject == QuestObject)
        {
            if (isCutSceneNeeded) InspectToAllowCutsceneObject = PlayerInteractionScript.InteractableObject.GetComponent<InspectToAllowCutscene>();
            Destroy(QuestObject.GetComponent<Outline>());
            StartCoroutine(BugDelay());
        }

        return this;
    }

    IEnumerator PromptDelay()
    {
        yield return new WaitForSeconds(120f);
        if (!isQuestDone) QuestObject.AddComponent<Outline>().color = 0;
    }

    IEnumerator BugDelay()
    {
        yield return new WaitForSeconds(2f);
        isQuestDone = true;
    }
}
