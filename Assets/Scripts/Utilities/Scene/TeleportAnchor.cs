using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class TeleportAnchor : MonoBehaviour
{
    public void Start()
    {
        switch (true)
        {
            case bool x when GameManager.instance.player != null:
                StartCoroutine(Teleport());
                break;
        }
    }

    public IEnumerator Teleport()
    {
        CharacterController cc = GameManager.instance.player.GetComponent<CharacterController>();
        cc.enabled = false;
        yield return new WaitForEndOfFrame();
        GameManager.instance.player.transform.position = gameObject.transform.position;
        //GameManager.instance.player.transform.rotation = gameObject.transform.rotation;
        yield return new WaitForEndOfFrame();
        cc.enabled = true;
    }
}
