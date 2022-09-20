using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Identity")]
public class DialogueIdentity : ScriptableObject
{
    [Header("Information")]
    public Sprite portrait;
    public string charName;
    public List<string> dialogue;
}
