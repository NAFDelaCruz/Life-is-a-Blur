using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectQuest : Quest
{
    public List<GameObject> TargetGameObjects;
    public Rigidbody PlayerRb;
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

        if (TargetGameObjects.Contains(PlayerInteractionScript.InteractableObject))
        {
            if (Input.GetMouseButtonDown(0))
            {
                TargetGameObjects.Remove(PlayerInteractionScript.InteractableObject);
            }
        }
        
        if (TargetGameObjects.Count == 0 && !isQuestDone)
        {
            isQuestDone = true;
            SetValues(DialogueElementsExtra1);
            SetDialogueValues();
            DialogueManagerScript.StartDialogue();
            StartCoroutine(NextQuestDelay(NextQuest));
        }

        return this;
    }
}
