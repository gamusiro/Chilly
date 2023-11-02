using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameratest : MonoBehaviour
{
    public CinemachineVirtualCameraBase vcamera1;
    public CinemachineVirtualCameraBase vcamera2;
     
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            vcamera1.Priority = 0;
            vcamera2.Priority = 1;
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            vcamera1.Priority = 1;
            vcamera2.Priority = 0;
        }

    }
}
