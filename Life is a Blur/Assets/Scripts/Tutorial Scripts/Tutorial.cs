using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{
    public DialogueManager DialogueManagerScript;
    public TutorialManager TutorialManagerScript;
    public List<GameObject> TutorialPrompts;
    public GameObject TutorialUI;
    public int TutorialIndex = 0;
    public bool isTutorialDone = false;

    public abstract Tutorial TutorialActions();

    private void Start()
    {
        TutorialManagerScript = GameObject.Find("Game Manager").GetComponent<TutorialManager>();
        DialogueManagerScript = GameObject.Find("Game Manager").GetComponent<DialogueManager>();
    }

    public IEnumerator TutorialDelay(Tutorial NextTutorial)
    {
        yield return new WaitForSeconds(3f);
        if (NextTutorial) TutorialManagerScript.CurrentTutorial = NextTutorial;
    }
}
