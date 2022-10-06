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
                        GameManager.instance.LoadDelegate(GameManager.instance.LoadBoss(false));
                        break;
                    case TeleportState.Shop:
                        GameManager.instance.LoadDelegate(GameManager.instance.LoadShop());
                        Destroy(gameObject);
                        break;
                    case TeleportState.Final:
                        GameManager.instance.LoadDelegate(GameManager.instance.LoadFinal());
                        break;
                    case TeleportState.Outro:
                        GameManager.instance.LoadDelegate(GameManager.instance.LoadOutro());
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
    Final,
    Outro,
}