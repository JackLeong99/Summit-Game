using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIndentifier : MonoBehaviour
{
    public DialogueIdentity identity;

    public void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                DialogueController.instance.LoadDialogue(identity);
                break;
        }
    }
}
