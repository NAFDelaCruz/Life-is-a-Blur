using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationQuest : Quest
{
    public GameObject Player;
    bool isPlayerNear = false;
    bool isQuestDone = false;

    public override Quest QuestActions()
    {
        if (isPlayerNear && !isQuestDone)
        {
            isQuestDone = true;
            Destroy(QuestObject.GetComponent<Outline>());
            Player.transform.position = QuestObject.transform.position;
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.StartDialogue();

        }

        if (DialogueManagerScript.isDialogueDone && isQuestDone) StartCoroutine(NextQuestDelay());

        IEnumerator NextQuestDelay()
        {
            yield return new WaitForSeconds(5f);
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
