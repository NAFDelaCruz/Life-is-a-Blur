using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : Tutorial
{
    public Animator DoctorAnimator;
    public List<string> Dialogue;
    public GameObject Prescription;

    bool hasOutline = false;
    
    void Start()
    {
        GetGameManagerComponents();
        DialogueManagerScript.Dialogues = Dialogue;
        DialogueManagerScript.StartDialogue();
        DoctorAnimator.SetBool("IsTalkingSitting", true);
    }

    public override Tutorial TutorialActions()
    {
        if (DialogueManagerScript.isDialogueDone)
        {
            DoctorAnimator.SetBool("IsTalkingSitting", false);

            if (!hasOutline)
            {
                hasOutline = true;
                Prescription.AddComponent<InteractionTest>().isInteractable = true;
                Prescription.AddComponent<Outline>().color = 0;
            }
            
            if (Prescription == null) Debug.Log("Game End");
        }

        return this;
    }
}
