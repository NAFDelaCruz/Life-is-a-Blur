using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextDisplayScript : MonoBehaviour
{
    [Header("Text")]
    [Range(0.0f, 1.0f)]
    public float displayDelay;
    public string fullText;
    string currentText = "";

    [Header("TextBox")]
    [Range(0.0f, 1.0f)]
    public float currentLerp;
    [Range(0.0f, 1.0f)]
    public float lerpRate;
    public bool isDone = true;
    public bool isDisplayed = false;
    public TMP_Text Dialogue;
    CanvasGroup DialogueBox;

    private void Start()
    {
        DialogueBox = GetComponent<CanvasGroup>();
    }

    public void DisplayText()
    {
        currentText = "";
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            Dialogue.text = currentText;
            yield return new WaitForSeconds(displayDelay);
        }
    }

    public void FadeInTextBG()
    {
        isDone = false;
        isDisplayed = false;
    }
    public void FadeOutTextBG()
    {
        isDone = false;
        isDisplayed = true;
    }

    public void Update()
    {
        //Lerping
        if (!isDone)
        {
            DialogueBox.alpha = currentLerp;

            if (!isDisplayed)
                currentLerp = Mathf.Clamp(currentLerp += lerpRate * Time.deltaTime, 0f, 1f);
            if (isDisplayed)
                currentLerp = Mathf.Clamp(currentLerp -= lerpRate * Time.deltaTime, 0f, 1f);
        }
    }
}
