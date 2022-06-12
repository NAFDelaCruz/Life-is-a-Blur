using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DestinationQuest : Quest
{
    public List<string> EndDialogue;
    public PlayableDirector EndCutscene;
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
            DialogueManagerScript.Dialogues = EndDialogue;
            DialogueManagerScript.StartDialogue();
            EndCutscene.Play();
        }

        return this;
    }

    public void Next()
    {
        StartCoroutine(NextQuestDelay(NextQuest));
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
