using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartQuest : Tutorial
{
    public List<string> Dialogue1;
    public List<string> Dialogue2;
    public List<string> Dialogue3;
    public List<GameObject> ObjectsToLookAt;
    public PlayerInteraction PlayerInteractionScript;
    
    int CurrentObject = -1;

    private void Start()
    {
        GetGameManagerComponents();
        DialogueManagerScript.Dialogues = Dialogue1;
        DialogueManagerScript.StartDialogue();
    }

    public override Tutorial TutorialActions()
    {
        if (isTutorialDone)
        {
            if (DialogueManagerScript.isDialogueDone) StartCoroutine(TutorialDelay(NextTutorial));
        }

        if (DialogueManagerScript.isDialogueDone && !isTutorialDone)
        {
            if (CurrentObject == -1)
            {
                CurrentObject++;
                ObjectsToLookAt[0].AddComponent<Outline>().color = 0;
            }

            if (CurrentObject == 0 && PlayerInteractionScript.InteractableObject == ObjectsToLookAt[0])
            {
                NextObject();
                DialogueManagerScript.Dialogues = Dialogue2;
            }

            if (CurrentObject == 1 && PlayerInteractionScript.InteractableObject == ObjectsToLookAt[1])
            {
                NextObject();
                DialogueManagerScript.Dialogues = Dialogue3;
            }

            if (CurrentObject == 2 && PlayerInteractionScript.InteractableObject == ObjectsToLookAt[2])
            {
                NextObject();
                isTutorialDone = true;
            }
        }
            
        return this;
    }

    public void NextObject()
    {
        CurrentObject++;
        Destroy(ObjectsToLookAt[CurrentObject-1].GetComponent<Outline>());
        if (CurrentObject < 3)
        {
            DialogueManagerScript.StartDialogue();
            ObjectsToLookAt[CurrentObject].AddComponent<Outline>().color = 0;
        }
    }
}