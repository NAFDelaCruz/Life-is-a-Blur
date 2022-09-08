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
                SetValues(DialogueElementsExtra2);
                NextObject();
            }

            if (CurrentObject == 1 && PlayerInteractionScript.InteractableObject == ObjectsToLookAt[1])
            {
                SetValues(DialogueElementsExtra3);
                PlayerRb.constraints = ~RigidbodyConstraints.FreezePosition;
                isQuestDone = true;
                NextObject();
            }

            if (isQuestDone)
            {
                if (Input.GetKey(KeyCode.Escape))
                {
                    Cursor.lockState = CursorLockMode.Confined;
                    SceneManager.LoadScene(0);
                }

                EndText.SetActive(true);
            }
        }
            
        return this;
    }

    public void NextObject()
    {
        CurrentObject++;
        Destroy(ObjectsToLookAt[CurrentObject-1].GetComponent<Outline>());
        if (CurrentObject < 2)
        {
            SetDialogueValues();
            DialogueManagerScript.StartDialogue();
            ObjectsToLookAt[CurrentObject].AddComponent<Outline>().color = 0;
        }
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
