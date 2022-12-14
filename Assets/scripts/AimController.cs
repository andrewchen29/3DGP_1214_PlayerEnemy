using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimController : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform cam;

    // Start is called before the first frame update
    void Start()
    {
       //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        // cam.localEulerAngles = new Vector3(yAxis.Value, cam.localEulerAngles.y, cam.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
