using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Text")]
    public List<string> Dialogues;
    [Range(0.0f, 1.0f)]
    public float DisplayDelay;
    string CurrentText = "";

    [Header("TextBox")]
    [Range(0.0f, 1.0f)]
    public float CurrentLerp;
    [Range(0.0f, 1.0f)]
    public float LerpRate;
    public TMP_Text Dialogue;
    public CanvasGroup DialogueBox;
    int DialogueBoxState;
    string LastLine;

    public void StartDialogue()
    {
        LastLine = Dialogues[Dialogues.Count - 1];
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {

        foreach (string Line in Dialogues)
        {
            CurrentText = "";

            for (int i = 0; i <= Line.Length; i++)
            {
                CurrentText = Line.Substring(0, i);
                Dialogue.text = CurrentText;
                yield return new WaitForSeconds(DisplayDelay);
            }
        }

        yield return new WaitForSeconds(1f);
        DialogueBoxState = 2;
    }

    /*
    IEnumerator FadeInBG()
    {
        while (CurrentLerp != 1)
        {
            CurrentLerp = Mathf.Clamp(CurrentLerp += LerpRate * Time.deltaTime, 0f, 1f);
            DialogueBox.alpha = CurrentLerp;
        }
    }
    */

    public void FadeInBox()
    {
        DialogueBoxState = 1;
    }

    public void FadeOutBox()
    {
        DialogueBoxState = 2;
    }

    public void Update()
    {
        if (DialogueBoxState == 1 && CurrentLerp != 1)
        {
        }

        if (DialogueBoxState == 2 && CurrentLerp != 0)
        {
            CurrentLerp = Mathf.Clamp(CurrentLerp -= LerpRate * Time.deltaTime, 0f, 1f);
            DialogueBox.alpha = CurrentLerp;
        }

        if (DialogueBoxState == 1 && CurrentLerp == 1)
        {
            DialogueBoxState = 0;
            NextDialougeLine();
        }

        if (DialogueBoxState == 2 && CurrentLerp == 0)
            DialogueBoxState = 0;

        if (isCurrLineDone && Input.GetMouseButtonDown(0))
        {
            CurrentDialogueIndex++;
            NextDialougeLine();
        }
    }
}
