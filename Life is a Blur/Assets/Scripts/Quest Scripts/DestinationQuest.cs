using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationQuest : Quest
{
    public GameObject Player;
    bool isPlayerNear = false;
    bool isQuestDone = false;
    InitialTutorial TutorialScript;

    public void Awake()
    {
        TutorialScript = GameObject.Find("Game Manager").GetComponent<TutorialManager>().CurrentTutorial.GetComponent<InitialTutorial>();
        QuestObject.AddComponent<Outline>().color = 0;
    }

    public override Quest QuestActions()
    {
        if (isPlayerNear && !isQuestDone)
        {
            isQuestDone = true;
            Player.transform.position = QuestObject.transform.position;
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone && isQuestDone)
        {
            TutorialScript.toSquint = true;
            if (NextQuest) QuestManagerScript.CurrentQuest = NextQuest;
        }

        return this;
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
