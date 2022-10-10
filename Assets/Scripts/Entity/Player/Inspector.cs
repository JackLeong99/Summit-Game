using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    [Header("Values")]
    public float interactRange;

    public void Update()
    {
        Inspection();
    }

    public void Inspection()
    {
        Camera cam = GameManager.instance.mainCamera.GetComponentInChildren<Camera>();
        Vector3 mousePosition = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(mousePosition, cam.transform.forward, out hit, interactRange)) //Checks whether or not the raycast has hit anything.
        {
            switch (hit.collider.tag)
            {
                case "Shop":
                    var stand = hit.collider.gameObject.GetComponent<ShopHandler>();
                    stand.DisplayItem();

                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        stand.BuyItem();
                    }
                    break;
                default:
                    CheckDisplay();
                    break;
            }
        }
        else
        {
            CheckDisplay();
        }
    }

    public void CheckDisplay()
    {
        switch (true)
        {
            case bool x when ShopManager.instance != null && ShopManager.instance.displayParent.activeInHierarchy == true:
                ShopManager.instance.EnableDisplay(false);
                break;
        }
    }
}
