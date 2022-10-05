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
        while (GameManager.instance.inLoading) { yield return null; }

        yield return new WaitForSeconds(1.2f);

        for (int i = 0; i < introDialogue.Count; i++)
        {
            yield return AnnouncementHandler.instance.Announcement(introDialogue[i], introDuration[i]);
            yield return new WaitForSeconds(1);
        }

        yield return new WaitForEndOfFrame();
        GameManager.instance.LoadDelegate(GameManager.instance.LoadBoss(true));
    }
}
