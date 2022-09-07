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
        GameManager.instance.player.GetComponent<ThirdPersonController>()._Inactionable = true;
        yield return new WaitForEndOfFrame();
        GameManager.instance.player.transform.position = gameObject.transform.position;
        yield return new WaitForEndOfFrame();
        GameManager.instance.player.GetComponent<ThirdPersonController>()._Inactionable = false;
    }
}
