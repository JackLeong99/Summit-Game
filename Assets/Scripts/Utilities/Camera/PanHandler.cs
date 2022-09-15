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
    { // FOV needs to be changed to 60
        GameManager.instance.mainCamera.transform.SetPositionAndRotation(anchors[selectedIndex].anchor.position, anchors[selectedIndex].anchor.rotation);
    }

    public void Update()
    {
        FocusAnchor();

        if (Input.GetKeyDown(KeyCode.H))
        {
            selectedIndex = IndexLooper.Increment(selectedIndex, anchors.Count);
        }
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
}
