using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncementHandler : MonoBehaviour
{
    public static AnnouncementHandler instance;

    [Header("Fade")]
    public FadeController fade;

    [Header("References")]
    public TextMeshProUGUI text;
    public List<string> spriteCodes;

    public void Awake()
    {
        instance = this;
    }

    public void Announce(string announcementText, float duration)
    {
        StartCoroutine(Announcement(announcementText, duration));
    }

    public IEnumerator Announcement(string announcement, float duration)
    {
        text.text = spriteCodes[0] + announcement + spriteCodes[1];

        yield return fade.FadeIn();
        yield return new WaitForSeconds(duration);
        yield return fade.FadeOut();
    }
}
