using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectQuest : Quest
{
    public PlayerInteraction PlayerInteractionScript;
    public GameObject DemoEndNotif;

    public bool isQuestDone = false;
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


            if (isQuestDone)
            {
                DemoEndNotif.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            isQuestDone = true;
            Destroy(QuestObject.GetComponent<Outline>());
        }
        
        return this;
    }
}
