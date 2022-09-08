using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindQuest : Quest
{
    public PlayerInteraction PlayerInteractionScript;
    bool isItemActive = true;
    bool isQuestDone = false;
    
    private void Awake()
    {
        StartCoroutine(PromptDelay());
    }

    public override Quest QuestActions()
    { 
        if (Input.GetMouseButtonDown(0) && PlayerInteractionScript.ObjectBehavior)
        {
            isItemActive = false;
            isQuestDone = true;
            DialogueManagerScript.Dialogues = QuestDialogue;
            DialogueManagerScript.CharacterVoices = CharacterVoices;
            DialogueManagerScript.CharacterAnimators = CharacterAnimators;
            DialogueManagerScript.CharacterAnimations = CharacterAnimations;
            DialogueManagerScript.StartDialogue();
        }

        if (DialogueManagerScript.isDialogueDone && isQuestDone)
        {
            NextQuest.QuestObject.AddComponent<Outline>().color = 0;
            if (NextQuest) QuestManagerScript.CurrentQuest = NextQuest;
        }

        return this;
    }

    IEnumerator PromptDelay()
    {
        yield return new WaitForSeconds(120f);
        if (isItemActive) QuestObject.AddComponent<Outline>().color = 0;
    }
}
