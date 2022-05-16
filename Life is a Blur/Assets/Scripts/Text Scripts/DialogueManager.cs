using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
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
    public TMP_Text Dialogue;
    public CanvasGroup DialogueBox;
    [HideInInspector]
    public bool isDialogueDone = true;
    float CurrentLerp;
    bool isHTMLTag = false;

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

            for (int i = 0; i < Line.Length; i++)
            {
                char Character = Line[i];

                if (Character == '<')
                {
                    isHTMLTag = true;
                    continue;
                }
                if (Character == '>')
                {
                    isHTMLTag = false;
                    continue;
                }

                if (!isHTMLTag)
                {
                    CurrentText = CurrentText + Character;
                    Dialogue.text = CurrentText;
                    yield return new WaitForSeconds(DisplayDelay);
                }
            }
            Dialogue.text = Line;
            yield return new WaitForSeconds(4f);
        }
        
        yield return new WaitForSeconds(4f);
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
