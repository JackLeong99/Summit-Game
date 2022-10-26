using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementIdentifier : MonoBehaviour
{
    [Header("Attributes")]
    public AnnouncementIdentity firstTime;
    public List<AnnouncementIdentity> options;
    public GameObject teleporterPrefab;
    public Transform teleporterPoint;
    public bool isDeath = false;

    [Header("Values")]
    public bool spoke;

    public void OnTriggerEnter(Collider other)
    {
        if (spoke) { gameObject.GetComponent<SphereCollider>().enabled = false; return; }

        switch (other.tag)
        {
            case "Player":
                switch (isDeath ? DataManager.instance.firstDeath : DataManager.instance.firsTime)
                {
                    case false:
                        int index = Random.Range(0, options.Count);
                        StartCoroutine(Speak(options[index]));
                        break;
                    case true:
                        StartCoroutine(Speak(firstTime));

                        switch (isDeath)
                        {
                            case true:
                                DataManager.instance.firstDeath = false;
                                break;
                            case false:
                                DataManager.instance.firsTime = false;
                                break;
                        }

                        break;
                }
                spoke = true;
                break;
        }
    }

    public IEnumerator Speak(AnnouncementIdentity identity)
    {
        foreach (var item in identity.announcement)
        {
            yield return AnnouncementHandler.instance.Announcement(item.text, item.duration);
            yield return new WaitForSeconds(item.delay);
        }

        yield return new WaitForEndOfFrame();
        Instantiate(teleporterPrefab, teleporterPoint);
    }
}
