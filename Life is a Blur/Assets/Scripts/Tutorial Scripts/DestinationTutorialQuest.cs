using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DestinationTutorialQuest : Quest
{
    public PlayableDirector Cutscene;
    public GameObject Player;
    Rigidbody PlayerRb;
    PlayerMovement PlayerMovementScript;
    bool isPlayerNear = false;
    bool isQuestDone = false;
    bool isDialogueStarted = false;
    bool isEndDialogueStarted = false;
    bool hasOutline = false;

    private void Start()
    {
        PlayerRb = Player.GetComponent<Rigidbody>();
        PlayerMovementScript = Player.GetComponent<PlayerMovement>();
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

        if (DialogueManagerScript.isDialogueDone && !isQuestDone &&!hasOutline)
        {
            hasOutline = true;
            PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
            QuestObject.AddComponent<Outline>().color = 0;
        }

        if (isPlayerNear && !isQuestDone) isQuestDone = true;

        if (DialogueManagerScript.isDialogueDone && isQuestDone && !isEndDialogueStarted)
        {
            isEndDialogueStarted = true;
            PlayerMovementScript.enabled = false;
            Player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Player.transform.position = new Vector3(QuestObject.transform.position.x, Player.transform.position.y, QuestObject.transform.position.z);
            PlayerRb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            Destroy(gameObject.GetComponent<Outline>());
            SetValues(DialogueElementsExtra1);
            SetDialogueValues();
            DialogueManagerScript.StartDialogue();
            Cutscene.Play();
        }

        return this;
    }

    public void Next()
    {
        SetValues(DialogueElementsExtra2);
        SetDialogueValues();
        DialogueManagerScript.StartDialogue();
    }

    void OnTriggerEnter(Collider other)
    {
        isPlayerNear = true;
    }

    void OnTriggerExit(Collider other)
    {
        isPlayerNear = false;
    }

    
}
