using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ChartQuest : Quest
{
    public List<string> Dialogue1;
    public List<string> Dialogue2;
    public List<string> Dialogue3;
    public List<GameObject> ObjectsToLookAt;
    public Rigidbody PlayerRb;
    public PlayerInteraction PlayerInteractionScript;
    public PlayableDirector SwitchCutscene;
    public GameObject EndText;
     
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
                DialogueManagerScript.Dialogues = Dialogue2;
                NextObject();
            }

            if (CurrentObject == 1 && PlayerInteractionScript.InteractableObject == ObjectsToLookAt[1])
            {
                DialogueManagerScript.Dialogues = Dialogue3;
                PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
                NextObject();
                isQuestDone = true;
            }

            if (isQuestDone)
            {
                if (Input.GetKey(KeyCode.Escape))
                    Application.Quit();

                EndText.SetActive(true);
                StartCoroutine(NextQuestDelay(NextQuest));
            }
        }
            
        return this;
    }

    public void NextObject()
    {
        CurrentObject++;
        Destroy(ObjectsToLookAt[CurrentObject-1].GetComponent<Outline>());
        if (CurrentObject <= 2)
        {
            DialogueManagerScript.StartDialogue();
            ObjectsToLookAt[CurrentObject].AddComponent<Outline>().color = 0;
        }
    }

    IEnumerator DelayDialogue()
    {
        yield return new WaitForSeconds(5f);
        DialogueManagerScript.Dialogues = QuestDialogue;
        DialogueManagerScript.StartDialogue();
    }

    IEnumerator DelayCutsceneDialogue()
    {
        yield return new WaitForSeconds(37f);
        DialogueManagerScript.Dialogues = Dialogue1;
        DialogueManagerScript.StartDialogue();
        isCutsceneDialogueDone = true;
    }
}
