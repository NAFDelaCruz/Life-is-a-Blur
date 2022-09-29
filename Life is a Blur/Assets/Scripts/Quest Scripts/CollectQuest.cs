using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollectQuest : Quest
{
    public List<GameObject> TargetGameObjects;
    public PlayerInteraction PlayerInteractionScript;

    bool isDialogueStarted = false;
    bool isQuestDone = false;

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

        if (TargetGameObjects.Count > 0 && TargetGameObjects.Contains(PlayerInteractionScript.InteractableObject))
        {
            if (Input.GetMouseButtonDown(0))
            {
                TargetGameObjects.Remove(PlayerInteractionScript.InteractableObject);
                Destroy(PlayerInteractionScript.InteractableObject);
            }
        }
        
        if (TargetGameObjects.Count == 0 && !isQuestDone && isDialogueStarted && DialogueManagerScript.isDialogueDone)
        {
            isQuestDone = true;
            StartCoroutine(NextQuestDelay(NextQuest));
        }

        return this;
    }

    IEnumerator PromptDelay()
    {
        yield return new WaitForSeconds(120f);
        if (!isQuestDone)
        {
            foreach (GameObject item in TargetGameObjects)
            {
                item.AddComponent<Outline>().color = 0;
            }
        }
    }
}
