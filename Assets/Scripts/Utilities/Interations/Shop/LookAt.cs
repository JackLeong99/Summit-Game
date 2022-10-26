using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform look;
    public float speed;

    public void Update()
    {
        Transform target = GameManager.instance.player.transform;
        Vector3 targetDirection = target.position - look.position;
        float rotation = speed * Time.deltaTime;

        Vector3 direction = Vector3.RotateTowards(look.forward, targetDirection, rotation, 35.0f);
        look.rotation = Quaternion.LookRotation(direction);
    }
}
