using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueController : MonoBehaviour
{
    public static DialogueController instance;

    [Header("Modifiable")]
    public float[] interval = new float[2];

    [Header("References")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI dialogueHeader;
    public Image dialoguePortrait;
    public GameObject dialogueParent;

    [Header("Values")]
    public int intervalIndex;
    public List<string> dialogue = new List<string>();
    public int index;
    public string display;
    public DialogueState dialogueState = DialogueState.Idle;

    public void Awake()
    {
        instance = this;
    }

    public void Update()
    {

        if (dialogueState == DialogueState.Load)
        {
            StartCoroutine(DisplayText(dialogue[index]));
            dialogueState = DialogueState.Normal;
        }

        if (Input.GetMouseButtonDown(0) && dialogueParent.activeInHierarchy)
        {
            if (index < dialogue.Count)
            {
                switch (dialogueState)
                {
                    case DialogueState.Idle:
                        StartCoroutine(DisplayText(dialogue[index]));
                        dialogueState = DialogueState.Normal;
                        break;
                    case DialogueState.Normal:
                        intervalIndex = 1;
                        dialogueState = DialogueState.Fast;
                        break;
                }
            }
            else
            {
                ClearDialogue();
            }
        }
    }

    public void LoadDialogue(DialogueIdentity identity)
    {
        dialoguePortrait.sprite = identity.portrait;
        dialogueHeader.text = identity.charName;
        dialogue = identity.dialogue;
        dialogueState = DialogueState.Load;
    }

    public void ClearDialogue()
    {
        dialogue.Clear();
        intervalIndex = 0;
        index = 0;
        dialogueParent.SetActive(false);
    }

    public void UpdateText()
    {
        dialogueText.text = display;
    }

    public IEnumerator DisplayText(string sentence)
    {
        for (int i = 0; i <= sentence.Length; i++)
        {
            display = sentence.Substring(0, i);
            UpdateText();
            yield return new WaitForSeconds(interval[intervalIndex]);
        }

        index++;
        intervalIndex = 0;
        dialogueState = DialogueState.Idle;
    }
}

public enum DialogueState
{
    Load,
    Idle,
    Normal,
    Fast
}