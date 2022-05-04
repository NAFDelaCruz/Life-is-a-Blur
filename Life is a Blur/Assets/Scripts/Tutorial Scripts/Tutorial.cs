using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{
    public List<GameObject> TutorialPrompts;
    public GameObject TutorialUI;
    public int TutorialIndex = 0;
    public bool isTutorialDone = true;

    public abstract Tutorial TutorialActions();

    public IEnumerator TutorialDelay(GameObject PrevTutorial, GameObject NextTutorial)
    {
        isTutorialDone = false;
        yield return new WaitForSeconds(2f);
        PrevTutorial.SetActive(false);
        yield return new WaitForSeconds(1f);
        NextTutorial.SetActive(true);
        isTutorialDone = true;
    }
}
