using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPeePeePooPoo : MonoBehaviour
{
    public GameObject XD;

    public void Start()
    {
        int xPos=Random.Range(-50, 50);
        int zPos=Random.Range(-50, 50);
        this.transform.position=new Vector3(xPos, 1, zPos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            XD.gameObject.SetActive(false);
            Destroy(XD);
        }
    }
}
