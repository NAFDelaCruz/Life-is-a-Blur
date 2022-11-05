using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenPostTest : MonoBehaviour
{
    public void OpenLink()
    {
        Application.OpenURL("https://forms.gle/RrwjG2e6eRcX2h9R6");
        PlayerPrefs.SetInt("Current Level", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(0);
    }
}
