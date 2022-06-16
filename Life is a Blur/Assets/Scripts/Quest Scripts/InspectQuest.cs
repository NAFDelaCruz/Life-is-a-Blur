using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class InspectQuest : Quest
{
    public PlayerInteraction PlayerInteractionScript;
    public PlayableDirector ThisCutScene;
    public PlayerMovement PlayerMovementScript;

    bool isDialogueStarted;
    bool isQuestDone;

    private void Start()
    {
        GetGameManagerComponents();
        StartCoroutine(PromptDelay());
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
            PlayerMovementScript.enabled = true;

            if (Input.GetMouseButtonDown(0) && PlayerInteractionScript.InteractableObject == QuestObject)
            {
                PlayerInteractionScript.InteractableObject.AddComponent<PlayCutsceneInteraction>().CutScene = ThisCutScene;
                PlayerInteractionScript.InteractableObject.GetComponent<PlayCutsceneInteraction>().isInteractable = true;
                Destroy(QuestObject.GetComponent<Outline>());
                isQuestDone = true;
            }
        }
        
        return this;
    }

    IEnumerator PromptDelay()
    {
        yield return new WaitForSeconds(120f);
        if (isQuestDone) QuestObject.AddComponent<Outline>().color = 0;
    }
}
