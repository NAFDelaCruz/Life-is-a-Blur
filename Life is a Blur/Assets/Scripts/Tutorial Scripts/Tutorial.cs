using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{
    public List<Sprite> TutorialPrompts;
    public GameObject TutorialUI;
    public int TutorialIndex = 0;
    public bool isTutorialDone = true;

    public abstract Tutorial TutorialActions();

    public IEnumerator TutorialDelay()
    {
        isTutorialDone = false;
        yield return new WaitForSeconds(3f);
        isTutorialDone = true;
    }
}
