using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizQuest : Quest
{
    public List<GameObject> ExactValues;
    public PlayerInteraction PlayerInteractionScript;

    bool isDialogueStarted = false;
    bool isFinalDialogueStarted = false;
    int Index = 0;
    int Score = 0;

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

        if (Index < ExactValues.Count && PlayerInteractionScript.InteractableObject)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (PlayerInteractionScript.InteractableObject == ExactValues[Index])
                {
                    Score++;
                }

                Index++;
                Debug.Log(Index);
            }
        }

        if (Index == ExactValues.Count && !isFinalDialogueStarted)
        {
            isFinalDialogueStarted = true;

            if (Score == 0)
                SetValues(DialogueElementsExtra1);
            else if (Score < ExactValues.Count/2)
                SetValues(DialogueElementsExtra2);
            else if (Score > ExactValues.Count/2)
                SetValues(DialogueElementsExtra3);

            SetDialogueValues();
            DialogueManagerScript.StartDialogue();
        }

        return this;
    }
}
