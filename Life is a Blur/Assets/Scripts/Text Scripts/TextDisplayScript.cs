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
    public Image textBackground;


    public void DisplayText()
    {
        currentText = "";
        this.GetComponent<TMP_Text>().color = Color.white;
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            this.GetComponent<TMP_Text>().text = currentText;
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
            Debug.Log("Lerp to black");
            textBackground.color = Color.Lerp(Color.clear, Color.black, currentLerp);
            if (!isDisplayed)
            {
                currentLerp += lerpRate * Time.deltaTime;
            }
            if (isDisplayed)
            {
                currentLerp -= lerpRate * Time.deltaTime;
                this.GetComponent<TMP_Text>().color = Color.Lerp(Color.clear, Color.white, currentLerp);
            }
            if (currentLerp <= 0)
            {
                isDone = true;
                currentLerp = 0;
            }
            if (currentLerp >= 1)
            {
                isDone = true;
                currentLerp = 1;
            }
        }
    }
}
