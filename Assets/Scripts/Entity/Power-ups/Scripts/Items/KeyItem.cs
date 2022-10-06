using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerups/KeyStoryItem")]

public class KeyItem : PassiveItem
{
    public AnnouncementIdentity identity;

    public Vector3 portalPos;
    public GameObject portalPrefab;

    public override void effect()
    {
        Destroy(GameObject.FindWithTag("Teleporter"));
        GameManager.instance.StartCoroutine(Final());
    }

    public IEnumerator Final()
    {
        yield return new WaitForSeconds(1);

        foreach (var item in identity.announcement)
        {
            yield return AnnouncementHandler.instance.Announcement(item.text, item.duration);
            yield return new WaitForSeconds(item.duration);
        }

        GameObject portal = Instantiate(portalPrefab, portalPos, Quaternion.identity, GameManager.instance.player.transform);
        portal.transform.parent = null;
    }

    public override void acquire()
    {
        Inventory inv = GameManager.instance.player.GetComponent<Inventory>();
        inv.keyItem = this;
        effect();
    }
}
