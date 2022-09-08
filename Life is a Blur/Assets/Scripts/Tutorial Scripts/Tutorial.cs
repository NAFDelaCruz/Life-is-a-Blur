using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TutorialDialogueElements
{
    [Tooltip("Paste here the dialogue lines along with the corresponding character. Example = Teacher: [Dialogue].")]
    public string TutorialDialogue;
    [Tooltip("Drag the audio source of the corresponding character here. Leave blank for Claro.")]
    public AudioSource CharacterVoices;
    [Tooltip("Drag the animator of the corresponding character here. Leave blank for Claro.")]
    public Animator CharacterAnimators;
    [Tooltip("Type the string parameter of the corresponding animation here. Leave blank for Claro.")]
    public AnimatorParameters CharacterAnimations;
}

public abstract class Tutorial : MonoBehaviour
{
    [HideInInspector]
    public List<string> TutorialDialogue;
    [HideInInspector]
    public List<AudioSource> CharacterVoices;
    [HideInInspector]
    public List<Animator> CharacterAnimators;
    [HideInInspector]
    public List<AnimatorParameters> CharacterAnimations;
    [HideInInspector]
    public DialogueManager DialogueManagerScript;
    [HideInInspector]
    public TutorialManager TutorialManagerScript;

    public TutorialDialogueElements[] DialogueElements;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public TutorialDialogueElements[] DialogueElementsExtra1;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public TutorialDialogueElements[] DialogueElementsExtra2;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public QuestDialogueElements[] DialogueElementsExtra3;

    public List<GameObject> TutorialPrompts;
    public GameObject TutorialUI;
    public Tutorial NextTutorial;
    public Rigidbody PlayerRb;
    public int TutorialIndex = 0;
    public bool isTutorialDone = false;

    public abstract Tutorial TutorialActions();

    public void GetGameManagerComponents()
    {
        DialogueManagerScript = GameObject.Find("Game Manager").GetComponent<DialogueManager>();
        TutorialManagerScript = GameObject.Find("Game Manager").GetComponent<TutorialManager>();
    }

    public void SetValues(TutorialDialogueElements[] ThisElement)
    {
        TutorialDialogue.Clear();
        CharacterVoices.Clear();
        CharacterAnimators.Clear();
        CharacterAnimations.Clear();

        foreach (TutorialDialogueElements Element in ThisElement)
        {
            TutorialDialogue.Add(Element.TutorialDialogue);
            CharacterVoices.Add(Element.CharacterVoices);
            CharacterAnimators.Add(Element.CharacterAnimators);
            CharacterAnimations.Add(Element.CharacterAnimations);
        }
    }

    public void SetDialogueValues()
    {
        DialogueManagerScript.Dialogues = TutorialDialogue;
        DialogueManagerScript.CharacterVoices = CharacterVoices;
        DialogueManagerScript.CharacterAnimators = CharacterAnimators;
        DialogueManagerScript.CharacterAnimations = CharacterAnimations;
    }

    public IEnumerator TutorialDelay(Tutorial NextTutorial)
    {
        yield return new WaitForSeconds(1f);
        if (NextTutorial) TutorialManagerScript.CurrentTutorial = NextTutorial;
    }
}
