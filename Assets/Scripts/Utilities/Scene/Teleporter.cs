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
                        SceneHandler.SwapScenes(GameManager.instance.bossScenes, GameManager.instance.exclusionScenes);
                        break;
                    case TeleportState.Shop:
                        GameManager.instance.LoadShop();
                        Destroy(gameObject);
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