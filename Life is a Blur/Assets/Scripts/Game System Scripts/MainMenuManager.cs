using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Camera Values")]
    public Transform Target;
    public Transform MainCamera;
    public PlayableDirector StartCutscene;
    public float TransitionSpeed;
    [Header("Canvas Values")]
    public GameObject MenuUI;
    public GameObject GameUI;
    public GameObject ExitMenu;
    public GameObject SettingsMenu;
    public GameObject NewGameBtn;
    public GameObject ResumeBtn;
    public GameObject ContinueBtn;
    public TextMeshProUGUI VolumeText;
    public TextMeshProUGUI MouseSenseText;
    [Header("Script Values")]
    public TutorialManager TutorialManagerScript;
    public PlayerMovement PlayerMovementScript;
    [Header("Settings")]
    public Slider Volume;
    public Slider Sensitivity;

    bool isGameStarted = false;
    bool menuToggle = false;

    private void Awake()
    {
        PlayerMovementScript.PlayerLookSensitivity = Sensitivity.value;
        AudioListener.volume = Volume.value;
        VolumeText.text = Mathf.Round(Volume.value * 100).ToString() + "%";
        MouseSenseText.text = (Mathf.Round(Sensitivity.value * 100) / 100).ToString();
        Volume.value = PlayerPrefs.GetFloat("Game Volume");
        Sensitivity.value = PlayerPrefs.GetFloat("Look Sensitivity");
        Cursor.lockState = CursorLockMode.Confined;
        if (PlayerPrefs.GetInt("Current Level") != 0 && ContinueBtn)
        {
            ContinueBtn.GetComponent<Button>().interactable = true;
            ContinueBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.white;
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Current Level"));
        Cursor.visible = false;
    }

    public void OpenMenu()
    {
        if (!menuToggle)
        {
            Cursor.visible = true;
            Time.timeScale = 0f;
            AudioListener.pause = true;
            PlayerMovementScript.isGameNotPaused = false;
            MenuUI.SetActive(!MenuUI.activeSelf);
            menuToggle = true;
        }
        else if (menuToggle)
        {
            Cursor.visible = false;
            Time.timeScale = 1f;
            AudioListener.pause = false;
            PlayerMovementScript.isGameNotPaused = true;
            MenuUI.SetActive(!MenuUI.activeSelf);
            menuToggle = false;
        }
    }

    public void SettingsPrompt()
    {
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
    }

    public void SaveSettingValues()
    {
        PlayerMovementScript.PlayerLookSensitivity = Sensitivity.value;
        AudioListener.volume = Volume.value;
        VolumeText.text = Mathf.Round(Volume.value * 100).ToString() + "%";
        MouseSenseText.text = (Mathf.Round(Sensitivity.value * 100) / 100).ToString();
        PlayerPrefs.SetFloat("Game Volume", Volume.value);
        PlayerPrefs.SetFloat("Look Sensitivity", Sensitivity.value);
    }
    
    public void ExitPrompt()
    {
        ExitMenu.SetActive(!ExitMenu.activeSelf);
    }

    public void ExitGame()
    {
        PlayerPrefs.SetInt("Current Level", SceneManager.GetActiveScene().buildIndex);
        Application.Quit();
    }

    public void StartGame()
    {
        Cursor.visible = false;
        isGameStarted = true;
        MenuUI.SetActive(false);
        NewGameBtn.SetActive(false);
        ContinueBtn.SetActive(false);
        GameUI.SetActive(true);
        ResumeBtn.SetActive(true);
        TutorialManagerScript.enabled = true;
        StartCutscene?.Play();
    }

    void Update()
    {
        if (isGameStarted)
        {
            MainCamera.position = Vector3.Lerp(MainCamera.position, Target.position, Time.deltaTime * TransitionSpeed);

            Vector3 currentAngle = new Vector3(
                Mathf.LerpAngle(MainCamera.rotation.eulerAngles.x, Target.transform.rotation.eulerAngles.x, Time.deltaTime * TransitionSpeed),
                Mathf.LerpAngle(MainCamera.rotation.eulerAngles.y, Target.transform.rotation.eulerAngles.y, Time.deltaTime * TransitionSpeed),
                Mathf.LerpAngle(MainCamera.rotation.eulerAngles.z, Target.transform.rotation.eulerAngles.z, Time.deltaTime * TransitionSpeed)
                );

            MainCamera.eulerAngles = currentAngle;
            if (MainCamera.position.x <= Target.position.x + 0.1f) isGameStarted = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && !ExitMenu.activeSelf && !SettingsMenu.activeSelf) OpenMenu();
    }
}
