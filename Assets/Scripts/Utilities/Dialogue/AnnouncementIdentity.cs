using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Announcement")]
[Serializable]
public class AnnouncementIdentity : ScriptableObject
{
    public List<Announcement> announcement;
}

[Serializable]
public class Announcement
{
    public string text;
    public float duration;
    public float delay;
}