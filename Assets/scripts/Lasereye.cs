using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasereye : MonoBehaviour
{
    public GameObject laserPrefab;
    public float laserSpeed = 5f;

    private void Update()
    {
     if(Input.GetMouseButtonDown(2))
        {
            GameObject laserclone;
            laserclone = Instantiate(laserPrefab, transform.position, laserPrefab.transform.rotation);
            laserclone.transform.Rotate(0, 0, 90);
            laserclone.transform.eulerAngles = new Vector3(90, 90, 90);
            laserclone.GetComponent<Rigidbody>().velocity = this.transform.right * laserSpeed;
        }   
    }

}
