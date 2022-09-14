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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        Announce(spriteCodes[0] + " Hello gamer " + spriteCodes[1], 5);
    }

    public void Announce(string announcementText, float duration)
    {
        StartCoroutine(Announcement(announcementText, duration));
    }

    public IEnumerator Announcement(string announcement, float duration)
    {
        text.text = announcement;

        yield return fade.FadeIn();
        yield return new WaitForSeconds(duration);
        fade.FadeOut();
    }
}
