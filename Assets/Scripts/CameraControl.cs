using SG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float mouseX;
    public float mouseY;

    CameraHandler cameraHandler;
    // Start is called before the first frame update
    private void Awake()
    {
        cameraHandler = CameraHandler.singleton;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime;

        if(cameraHandler != null)
        {
            cameraHandler.FollowTarget(delta);
            cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
        }
    }
}
