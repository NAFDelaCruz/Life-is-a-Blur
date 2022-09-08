using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MainMenuManager : MonoBehaviour
{
    public Transform Target;
    public Transform MainCamera;
    public float TransitionSpeed;
    public GameObject StartMenu;
    public GameObject GameUI;
    public TutorialManager TutorialManagerScript;
    public QuestManager QuestManagerScript;
    public PlayableDirector StartCutscene;

    bool isGameStarted = false;

    public void Exit()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isGameStarted = true;
        StartMenu.SetActive(false);
        GameUI.SetActive(true);
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
        }

        if (MainCamera.position.x <= Target.position.x + 0.1f) isGameStarted = false;
    }
}
