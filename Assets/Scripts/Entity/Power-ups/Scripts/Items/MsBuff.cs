using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[CreateAssetMenu(menuName = "Powerups/MovementSpeedBuff")]
public class MsBuff : ItemBase
{
    public float amount;
    public override void effect()
    {
        GameManager.instance.player.GetComponent<ThirdPersonController>().MoveSpeed += amount;
        GameManager.instance.player.GetComponent<ThirdPersonController>().SprintSpeed += amount;
    }
}
