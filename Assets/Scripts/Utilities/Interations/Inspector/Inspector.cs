using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    [Header("Values")]
    public float interactRange;
    public float alphaSpeed;
    private float targetAlpha;

    [Header("References")]
    public CanvasGroup display;

    public void Update()
    {
        Inspection();
        Alpha();
    }

    public void Inspection()
    {
        Camera cam = GameManager.instance.mainCamera.GetComponentInChildren<Camera>();
        Vector3 mousePosition = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(mousePosition, cam.transform.forward, out hit, interactRange)) //Checks whether or not the raycast has hit anything.
        {
            switch (true)
            {
                case bool x when hit.collider.gameObject.GetComponent<Interactable>():
                    var interact = hit.collider.gameObject.GetComponent<Interactable>();

                    targetAlpha = 1;

                    interact.Display();

                    if (GameManager.instance.menuInput.Interactions.Interact.WasPressedThisFrame())
                    {
                        interact.Invoke();
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
        targetAlpha = 0;

        switch (true)
        {
            case bool x when ShopManager.instance != null && ShopManager.instance.displayParent.activeInHierarchy == true:
                ShopManager.instance.EnableDisplay(false);
                break;
        }
    }

    public void Alpha()
    {
        display.alpha = Mathf.MoveTowards(display.alpha, targetAlpha, alphaSpeed * Time.deltaTime);
    }
}
