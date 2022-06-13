using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public GameObject QuestObject;
    public List<string> QuestDialogue;
    [HideInInspector]
    public DialogueManager DialogueManagerScript;
    [HideInInspector]
    public QuestManager QuestManagerScript;
    public Quest NextQuest;

    public abstract Quest QuestActions();

    public void GetGameManagerComponents()
    {
        DialogueManagerScript = GameObject.Find("Game Manager").GetComponent<DialogueManager>();
        QuestManagerScript = GameObject.Find("Game Manager").GetComponent<QuestManager>();
    }

    public IEnumerator NextQuestDelay(Quest NextQuest)
    {
        yield return new WaitForSeconds(1f);
        if (NextQuest) QuestManagerScript.CurrentQuest = NextQuest;
    }
}
