using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PanHandler : MonoBehaviour
{
    [Header("Values")]
    public int selectedIndex;

    [Header("Anchors")]
    public List<Transform> anchors;

    public void Start()
    { // FOV needs to be changed to 60
        GetAnchors();

        GameManager.instance.mainCamera.transform.SetPositionAndRotation(anchors[selectedIndex].position, anchors[selectedIndex].rotation);
    }

    public void GetAnchors()
    {
        anchors = GetComponentsInChildren<Transform>().ToList();
        anchors.RemoveAt(0);
        selectedIndex = 0;
    }

    public void Update()
    {
        FocusAnchor();

        if (Input.GetKeyDown(KeyCode.H))
            selectedIndex++;
    }

    public void FocusAnchor()
    {
        switch (true)
        {
            case bool x when anchors.Count > 0:
                GameManager.instance.mainCamera.transform.position = Vector3.Lerp(GameManager.instance.mainCamera.transform.position, anchors[selectedIndex].position, 1 * Time.deltaTime);
                GameManager.instance.mainCamera.transform.rotation = Quaternion.Lerp(GameManager.instance.mainCamera.transform.rotation, anchors[selectedIndex].rotation, 1 * Time.deltaTime);
                break;
        }
    }
}
