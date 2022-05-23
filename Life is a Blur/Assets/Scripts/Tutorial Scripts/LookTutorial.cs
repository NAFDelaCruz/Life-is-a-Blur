using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookTutorial : Tutorial
{
    public List<string> Dialogue1;
    public List<string> Dialogue2;
    public List<string> Dialogue3;
    public List<string> Dialogue4;
    public List<GameObject> ObjectsToLookAt;
    public PlayerInteraction PlayerInteractionScript;
    public Tutorial NextTutorial;
    
    int CurrentObject = 0;
    CanvasGroup CurrentTutorial;

    private void Start()
    {
        DialogueManagerScript.Dialogues = Dialogue1;
        DialogueManagerScript.StartDialogue();
        ObjectsToLookAt[0].AddComponent<Outline>().color = 0;
        CurrentTutorial = TutorialPrompts[TutorialIndex].GetComponent<CanvasGroup>();
    }

    public override Tutorial TutorialActions()
    {
        if (isTutorialDone)
        {
            CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha -= 0.1f);
            StartCoroutine(TutorialDelay(NextTutorial));
        }

        if (DialogueManagerScript.isDialogueDone)
        {
            CurrentTutorial.alpha = Mathf.Clamp01(CurrentTutorial.alpha += 0.1f);

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
                Destroy(ObjectsToLookAt[2].GetComponent<Outline>());
                DialogueManagerScript.Dialogues = Dialogue4;
                DialogueManagerScript.StartDialogue();
                isTutorialDone = true;
            }
        }
            
        return this;
    }

    public void NextObject()
    {
        CurrentObject++;
        DialogueManagerScript.StartDialogue();
        Destroy(ObjectsToLookAt[CurrentObject-1].GetComponent<Outline>());
        ObjectsToLookAt[CurrentObject].AddComponent<Outline>().color = 0;
    }
}
