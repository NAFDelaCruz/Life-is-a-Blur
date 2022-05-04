using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractQuest : Quest
{
    public GameObject InteractNotif;
    public GameObject Player;
    bool isDialogueStarted;

    public override Quest QuestActions()
    {
        if (!isDialogueStarted)
        {
            isDialogueStarted = true;
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone)
        {
            if (!QuestObject.GetComponent<Outline>()) QuestObject.AddComponent<Outline>().color = 0;

            float Distance = Vector3.Distance(Player.transform.position, QuestObject.transform.position);

            if (Distance < 5f)
            {
                InteractNotif.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    InteractNotif.SetActive(false);
                    Destroy(QuestObject.GetComponent<Outline>());
                    if (NextQuest) QuestManagerScript.CurrentQuest = NextQuest;
                }
            }
        }

        return this;
    }
}
