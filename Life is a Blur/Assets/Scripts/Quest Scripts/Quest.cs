using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : MonoBehaviour
{
    public GameObject QuestObject;
    public List<string> QuestDialogue;
    public DialogueManager DialogueManagerScript;
    public QuestManager QuestManagerScript;
    public Quest NextQuest;

    public abstract Quest QuestActions();

    private void Start()
    {
        DialogueManagerScript = GameObject.Find("Game Manager").GetComponent<DialogueManager>();
        QuestManagerScript = GameObject.Find("Game Manager").GetComponent<QuestManager>();
    }
}
