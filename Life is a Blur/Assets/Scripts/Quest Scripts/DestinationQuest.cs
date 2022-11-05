using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationQuest : Quest
{
    public PlayerMovement PlayerMovementScript;
    public Rigidbody PlayerRb;
    
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
            QuestObject.AddComponent<Outline>().color = 0;
            SetDialogueValues();
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone && !isQuestDone)
        {
            QuestObject.GetComponent<BoxCollider>().enabled = true;
            PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
            PlayerMovementScript.enabled = true;
        }

        if (isQuestDone && NextQuest && DialogueManagerScript.isDialogueDone) StartCoroutine(NextQuestDelay(NextQuest));

        return this;
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Collider>().enabled = false;
        SetValues(DialogueElementsExtra1);
        SetDialogueValues();
        DialogueManagerScript.StartDialogue();
        isQuestDone = true;
        PlayerRb.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(QuestObject.GetComponent<Outline>());
    }
}