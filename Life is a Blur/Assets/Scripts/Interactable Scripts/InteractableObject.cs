using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InteractableDialogueElements
{
    [Tooltip("Paste here the dialogue lines along with the corresponding character. Example = Teacher: [Dialogue].")]
    public string CharacterName;
    [Tooltip("Paste here the dialogue lines along with the corresponding character. Example = Teacher: [Dialogue].")]
    public string InteractableDialogue;
    [Tooltip("Drag the audio source of the corresponding character here. Leave blank for Claro.")]
    public AudioSource CharacterVoices;
    [Tooltip("Drag the animator of the corresponding character here. Leave blank for Claro.")]
    public Animator CharacterAnimators;
    [Tooltip("Select the corresponding animation here. Select None for Claro")]
    public AnimatorParameters CharacterAnimations;
}

public abstract class InteractableObject : MonoBehaviour
{
    public bool isInteractable;
    public bool isInspectable;

    public InteractableDialogueElements[] DialogueElements;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public InteractableDialogueElements[] DialogueElementsExtra1;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public InteractableDialogueElements[] DialogueElementsExtra2;
    [Tooltip("Fill in for extra dialogue sequences.")]
    public InteractableDialogueElements[] DialogueElementsExtra3;
    public GameObject QuestObject;
    [HideInInspector]
    public List<string> CharacterNames;
    [HideInInspector]
    public List<string> InteractableDialogue;
    [HideInInspector]
    public List<AudioSource> CharacterVoices;
    [HideInInspector]
    public List<Animator> CharacterAnimators;
    [HideInInspector]
    public List<AnimatorParameters> CharacterAnimations;
    [HideInInspector]
    public DialogueManager DialogueManagerScript;

    public abstract InteractableObject Interact();

    public void GetGameManagerComponents()
    {
        DialogueManagerScript = GameObject.Find("Game Manager").GetComponent<DialogueManager>();
    }

    public void SetValues(InteractableDialogueElements[] ThisElement)
    {
        CharacterNames.Clear();
        InteractableDialogue.Clear();
        CharacterVoices.Clear();
        CharacterAnimators.Clear();
        CharacterAnimations.Clear();

        foreach (InteractableDialogueElements Element in ThisElement)
        {
            CharacterNames.Add(Element.CharacterName);
            InteractableDialogue.Add(Element.InteractableDialogue);
            CharacterVoices.Add(Element.CharacterVoices);
            CharacterAnimators.Add(Element.CharacterAnimators);
            CharacterAnimations.Add(Element.CharacterAnimations);
        }
    }

    public void SetDialogueValues()
    {
        DialogueManagerScript.CharacterNames = CharacterNames;
        DialogueManagerScript.Dialogues = InteractableDialogue;
        DialogueManagerScript.CharacterVoices = CharacterVoices;
        DialogueManagerScript.CharacterAnimators = CharacterAnimators;
        DialogueManagerScript.CharacterAnimations = CharacterAnimations;
    }
}
