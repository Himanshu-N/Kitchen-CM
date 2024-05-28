using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        lookAt,
        lookAtInverted,
        cameraForward,
        cameraForwardInverted,
    }

    [SerializeField] private Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.lookAt:
                transform.LookAt(Camera.main.transform); // position of camera will affect it
                break;
            case Mode.lookAtInverted:
                Vector3 DirToCam = transform.position - Camera.main.transform.position;
                transform.LookAt(DirToCam + transform.position); // check it on graph.. look at works assuming itself the centre for the constructing vector using the position cordinate
                break;
            case Mode.cameraForward:
                transform.forward = Camera.main.transform.forward; //now rotation of camera will affect it .. since the local vector remains the same even after changing camera's position
                break;
            case Mode.cameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;

        }
    }
}
