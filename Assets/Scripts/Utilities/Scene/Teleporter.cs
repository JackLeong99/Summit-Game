using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("What you want to teleport to")]
    public TeleportState state;

     private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                switch (state)
                {
                    case TeleportState.Boss:
                        //SceneHandler.SwapScenes(GameManager.instance.bossScenes, GameManager.instance.exclusionScenes);
                        SceneHandler.SwapScenes(GameManager.instance.showcase, GameManager.instance.exclusionScenes);
                        HealthbarManager.instance.SetActive(true);

                        break;
                    case TeleportState.Shop:
                        GameManager.instance.LoadShop();
                        HealthbarManager.instance.SetActive(false);
                        break;
                }
                break;
        }
     }
}

public enum TeleportState
{ 
    Shop,
    Boss,
}