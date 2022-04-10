using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTarget : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame
    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "PlayerBullet")
        {
            Destroy (gameObject);
        }
    }
}
