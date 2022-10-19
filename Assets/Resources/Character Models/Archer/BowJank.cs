using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowJank : MonoBehaviour
{
    public Animator bowAnim;

    public void doShoot() 
    {
        Debug.Log("JankSHootHappened");
        bowAnim.SetTrigger("Shoot");
    }
}
