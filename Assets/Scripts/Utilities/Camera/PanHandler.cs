using System;
using System.Collections.Generic;
using UnityEngine;

public class PanHandler : MonoBehaviour
{
    [Header("Values")]
    public int selectedIndex;

    [Header("Anchors")]
    public List<Anchor> anchors;

    [Serializable] public class Anchor
    {
        public Transform anchor;
        public float speed;
    }

    public void Start()
    {
        GameManager.instance.mainCamera.GetComponent<Camera>().fieldOfView = 60;
        GameManager.instance.mainCamera.transform.SetPositionAndRotation(anchors[selectedIndex].anchor.position, anchors[selectedIndex].anchor.rotation);
    }

    public void Update()
    {
        FocusAnchor();
    }

    public void FocusAnchor()
    {
        switch (true)
        {
            case bool x when anchors.Count > 0:
                GameManager.instance.mainCamera.transform.position = Vector3.Lerp(GameManager.instance.mainCamera.transform.position, anchors[selectedIndex].anchor.position, anchors[selectedIndex].speed * Time.deltaTime);
                GameManager.instance.mainCamera.transform.rotation = Quaternion.Lerp(GameManager.instance.mainCamera.transform.rotation, anchors[selectedIndex].anchor.rotation, anchors[selectedIndex].speed * Time.deltaTime);
                break;
        }
    }

    public void SwapFocus(int index)
    {
        switch (index)
        {
            case int x when x < anchors.Count:
                selectedIndex = index;
                break;
        }
    }
}