using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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
        Handle();
        yield return new WaitForEndOfFrame();
        cc.enabled = true;
    }

    public void Handle()
    {
        GameManager.instance.player.transform.position = gameObject.transform.position;
        GameManager.instance.player.transform.rotation = gameObject.transform.rotation;

        /*var cinemachine = GameObject.FindWithTag("Cinemachine");
        cinemachine.GetComponent<CinemachineVirtualCamera>().PreviousStateIsValid = false;
        cinemachine.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<Cinemachine3rdPersonFollow>().ForceCameraPosition(gameObject.transform.position, gameObject.transform.rotation);
        cinemachine.transform.position = gameObject.transform.position;
        cinemachine.transform.rotation = gameObject.transform.rotation;
        GameManager.instance.mainCamera.transform.position = gameObject.transform.position;
        GameManager.instance.mainCamera.transform.rotation = gameObject.transform.rotation;
        cinemachine.GetComponent<CinemachineVirtualCamera>().PreviousStateIsValid = true;*/

    }
}
