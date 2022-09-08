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
    
    [Header("Dialogue Variables")]
    public List<AudioSource> CharacterVoices;
    public List<Animator> CharacterAnimators;
    public List<AnimatorParameters> CharacterAnimations;

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

        for (int index1 = 0; index1 < Dialogues.Count; index1++)
        {
            CurrentText = "";
            if (CharacterVoices[index1]) CharacterVoices[index1].Play();
            if (CharacterAnimators[index1]) CharacterAnimators[index1].SetBool(CharacterAnimations[index1].ToString(), true);

            for (int index2 = 0; index2 < Dialogues[index1].Length; index2++)
            {
                char Character = Dialogues[index1][index2];

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

            Dialogue.text = Dialogues[index1];

            yield return new WaitForSeconds(4f);

            if (CharacterVoices[index1]) CharacterVoices[index1]?.Stop();
            if (CharacterAnimators[index1]) CharacterAnimators[index1]?.SetBool(CharacterAnimations[index1].ToString(), false);
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
