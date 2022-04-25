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
    [Range(0.0f, 0.1f)]
    public float LerpRate;
    float CurrentLerp;
    public TMP_Text Dialogue;
    public CanvasGroup DialogueBox;
    [HideInInspector]
    public bool isDialogueDone = true; 

    public void StartDialogue()
    {
        CurrentText = "";
        Dialogue.text = CurrentText;
        isDialogueDone = false;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        StartCoroutine(FadeInBG());
        yield return new WaitForSeconds(1f);

        foreach (string Line in Dialogues)
        {
            CurrentText = "";

            for (int i = 0; i <= Line.Length; i++)
            {
                CurrentText = Line.Substring(0, i);
                Dialogue.text = CurrentText;
                yield return new WaitForSeconds(DisplayDelay);
            }
            
            yield return new WaitForSeconds(1.75f);
        }
        
        yield return new WaitForSeconds(1.75f);
        StartCoroutine(FadeOutBG());
    }
    
    IEnumerator FadeInBG()
    {
        while (CurrentLerp != 1)
        {
            CurrentLerp = Mathf.Clamp(CurrentLerp += LerpRate, 0f, 1f);
            DialogueBox.alpha = CurrentLerp;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator FadeOutBG()
    {
        while (CurrentLerp != 0)
        {
            CurrentLerp = Mathf.Clamp(CurrentLerp -= LerpRate, 0f, 1f);
            DialogueBox.alpha = CurrentLerp;
            yield return new WaitForSeconds(0.01f);
        }

        isDialogueDone = true;
    }
}
