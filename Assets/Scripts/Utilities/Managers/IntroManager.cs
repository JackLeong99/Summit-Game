using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public List<string> introDialogue;
    public List<int> introDuration;

    public void Start()
    {
        StartCoroutine(PlayIntro());
    }

    public IEnumerator PlayIntro()
    {
        for (int i = 0; i < introDialogue.Count; i++)
        {
            yield return AnnouncementHandler.instance.Announcement(introDialogue[i], introDuration[i]);
        }

        yield return new WaitForEndOfFrame();
        GameManager.instance.LoadBoss();
    }
}
