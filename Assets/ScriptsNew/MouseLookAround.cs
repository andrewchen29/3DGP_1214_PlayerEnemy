using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAround : MonoBehaviour
{
    float Xrotation = 0f;
    float Yrotation = 0f;

    public float sensitivity = 10f;

    private void Update()
    {
        Xrotation += Input.GetAxis("Mouse X") * sensitivity;
        Yrotation += Input.GetAxis("Mouse Y") * -1 *  sensitivity;
        transform.localEulerAngles = new Vector3(Xrotation, Yrotation, 0);
    }
}
