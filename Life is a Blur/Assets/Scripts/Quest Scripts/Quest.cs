using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestDialogueElements
{
    [Tooltip("Paste here the dialogue lines along with the corresponding character. Example = Teacher: [Dialogue].")]
    public string QuestDialogue;
    [Tooltip("Drag the audio source of the corresponding character here. Leave blank for Claro.")]
    public AudioSource CharacterVoices;
    [Tooltip("Drag the animator of the corresponding character here. Leave blank for Claro.")]
    public Animator CharacterAnimators;
    [Tooltip("Select the corresponding animation here. Select None for Claro")]
    public AnimatorParameters CharacterAnimations;
}

public enum AnimatorParameters
{
    None,
    IsTalkingSitting,
    IsPresenting,
    IsWalking,
    IsTalking,
    IsSitting
}

public abstract class Quest : MonoBehaviour
{
    public QuestDialogueElements[] DialogueElements;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public QuestDialogueElements[] DialogueElementsExtra1;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public QuestDialogueElements[] DialogueElementsExtra2;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public QuestDialogueElements[] DialogueElementsExtra3;
    public GameObject QuestObject;
    [HideInInspector]
    public List<string> QuestDialogue;
    [HideInInspector]
    public List<AudioSource> CharacterVoices;
    [HideInInspector]
    public List<Animator> CharacterAnimators;
    [HideInInspector]
    public List<AnimatorParameters> CharacterAnimations;
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

    public void SetValues(QuestDialogueElements[] ThisElement)
    {
        QuestDialogue.Clear();
        CharacterVoices.Clear();
        CharacterAnimators.Clear();
        CharacterAnimations.Clear();

        foreach (QuestDialogueElements Element in ThisElement)
        {
            QuestDialogue.Add(Element.QuestDialogue);
            CharacterVoices.Add(Element.CharacterVoices);
            CharacterAnimators.Add(Element.CharacterAnimators);
            CharacterAnimations.Add(Element.CharacterAnimations);
        }
    }

    public void SetDialogueValues()
    {
        DialogueManagerScript.Dialogues = QuestDialogue;
        DialogueManagerScript.CharacterVoices = CharacterVoices;
        DialogueManagerScript.CharacterAnimators = CharacterAnimators;
        DialogueManagerScript.CharacterAnimations = CharacterAnimations;
    }

    public IEnumerator NextQuestDelay(Quest NextQuest)
    {
        yield return new WaitForSeconds(1f);
        if (NextQuest) QuestManagerScript.CurrentQuest = NextQuest;
    }
}
