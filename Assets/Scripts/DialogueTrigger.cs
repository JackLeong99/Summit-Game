using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
        
        //TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    // public void setDialogue(string[] dialogueBoxSentences)
    // {
    //     foreach (string sentence in dialogue.sentences)
    //     {
    //         dialogue.sentences = dialogueBoxSentences;
    //     }
        
    // }
}


