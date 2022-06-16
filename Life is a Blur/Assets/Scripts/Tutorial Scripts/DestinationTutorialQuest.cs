using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DestinationTutorialQuest : Quest
{
    public List<string> ExtraDialogue1;
    public List<string> ExtraDialogue2;
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
    }

    public override Quest QuestActions()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone && !isQuestDone &&!hasOutline)
        {
            hasOutline = true;
            PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
            QuestObject.AddComponent<Outline>().color = 0;
        }

        if (isPlayerNear && !isQuestDone)
        {
            isQuestDone = true;
            Destroy(QuestObject.GetComponent<Outline>());
        }

        if (DialogueManagerScript.isDialogueDone && isQuestDone && !isEndDialogueStarted)
        {
            isEndDialogueStarted = true;
            PlayerMovementScript.enabled = false;
            Player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            Player.transform.position = new Vector3(QuestObject.transform.position.x, Player.transform.position.y, QuestObject.transform.position.z);
            PlayerRb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            Destroy(gameObject.GetComponent<Outline>());
            DialogueManagerScript.Dialogues = ExtraDialogue1;
            DialogueManagerScript.StartDialogue();
            Cutscene.Play();
        }

        return this;
    }

    public void Next()
    {
        DialogueManagerScript.Dialogues = ExtraDialogue2;
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
