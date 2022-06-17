using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InspectQuest : Quest
{
    public PlayerInteraction PlayerInteractionScript;
    public PlayableDirector ThisCutScene;
    public PlayerMovement PlayerMovementScript;

    InspectToAllowCutscene InspectToAllowCutsceneObject;
    bool isDialogueStarted;
    bool isQuestDone;

    private void Start()
    {
        GetGameManagerComponents();
    }

    public override Quest QuestActions()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone)
        {
            StartCoroutine(PromptDelay());
            PlayerMovementScript.enabled = true;

            if (Input.GetMouseButtonDown(1) && PlayerInteractionScript.InteractableObject == QuestObject)
            {
                InspectToAllowCutsceneObject = PlayerInteractionScript.InteractableObject.GetComponent<InspectToAllowCutscene>();
                Destroy(QuestObject.GetComponent<Outline>());
                StartCoroutine(BugDelay());
            }

            if (isQuestDone && NextQuest)
            {
                InspectToAllowCutsceneObject.isInteractable = true;
                StartCoroutine(NextQuestDelay(NextQuest));
            }
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
