using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChartQuest : Quest
{
    public List<GameObject> ObjectsToLookAt;
    public Rigidbody PlayerRb;
    public PlayerInteraction PlayerInteractionScript;
    public PlayableDirector SwitchCutscene;
     
    int CurrentObject = -1;
    bool isDialogueStarted = false;
    bool isCutsceneDialogueDone = false;
    bool isQuestDone = false;

    private void Start()
    {
        GetGameManagerComponents();
    }

    public override Quest QuestActions()
    {
        if (!isDialogueStarted)
        {
            PlayerRb.constraints = RigidbodyConstraints.FreezeAll;
            isDialogueStarted = true;
            SwitchCutscene.Play();
            StartCoroutine(DelayDialogue());
            StartCoroutine(DelayCutsceneDialogue());
        }

        if (DialogueManagerScript.isDialogueDone && isCutsceneDialogueDone)
        {
            if (CurrentObject == -1)
            {
                CurrentObject++;
                ObjectsToLookAt[0].AddComponent<Outline>().color = 0;
            }

            if (CurrentObject == 0 && PlayerInteractionScript.InteractableObject == ObjectsToLookAt[0])
            {
                SetValues(DialogueElementsExtra2);
                NextObject();
                ObjectsToLookAt[1].AddComponent<Outline>().color = 0;
            }

            if (CurrentObject == 1 && PlayerInteractionScript.InteractableObject == ObjectsToLookAt[1])
            {
                SetValues(DialogueElementsExtra3);
                PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
                NextObject();
                isQuestDone = true;
            }

            if (isQuestDone && DialogueManagerScript.isDialogueDone)
            {
                StartCoroutine(NextQuestDelay(NextQuest));
            }
        }
            
        return this;
    }

    public void NextObject()
    {
        CurrentObject++;
        Destroy(ObjectsToLookAt[CurrentObject-1].GetComponent<Outline>());
        SetDialogueValues();
        DialogueManagerScript.StartDialogue();
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(5f);
        SetValues(DialogueElements);
        SetDialogueValues();
        DialogueManagerScript.StartDialogue();
    }

    IEnumerator DelayCutsceneDialogue()
    {
        yield return new WaitForSeconds(37f);
        SetValues(DialogueElementsExtra1);
        SetDialogueValues();
        DialogueManagerScript.StartDialogue();
        isCutsceneDialogueDone = true;
    }
}
