using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunArrow : ProjectileBase 
{
    private void Start()
    {
        Physics.IgnoreLayerCollision(9, 9);
    }
}
