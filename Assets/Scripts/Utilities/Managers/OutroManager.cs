using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroManager : MonoBehaviour
{
    public AnnouncementIdentity identity;
    public float waitUntil;

    public void Start()
    {
        StartCoroutine(PlayOutro());
    }

    public IEnumerator PlayOutro()
    {
        while (GameManager.instance.inLoading) { yield return null; }

        yield return new WaitForSeconds(waitUntil);

        foreach (var item in identity.announcement)
        {
            yield return AnnouncementHandler.instance.Announcement(item.text, item.duration);
            yield return new WaitForSeconds(item.duration);
        }

        yield return new WaitForEndOfFrame();
        GameManager.instance.LoadDelegate(GameManager.instance.QuitGame());
    }
}