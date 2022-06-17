using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationQuest : Quest
{
    public List<string> ArrivalDialogue;
    public PlayerMovement PlayerMovementScript;
    public Rigidbody PlayerRb;
    
    bool isDialogueStarted = false;
    bool isQuestDone = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GetGameManagerComponents();
    }

    public override Quest QuestActions()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            QuestObject.AddComponent<Outline>().color = 0;
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone)
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
        DialogueManagerScript.Dialogues = ArrivalDialogue;
        DialogueManagerScript.StartDialogue();
        isQuestDone = true;
        PlayerRb.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(QuestObject.GetComponent<Outline>());
    }
}