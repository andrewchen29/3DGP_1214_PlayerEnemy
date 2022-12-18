using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotState : MonoBehaviour
{
    public static float playerhealth=100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerhealth < 0)
            playerhealth = 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damage") playerhealth -= 10;
    }
}
