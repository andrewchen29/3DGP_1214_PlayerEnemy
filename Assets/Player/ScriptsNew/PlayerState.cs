using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static float Playerhealth = 100;

    // Update is called once per frame
    void Update()
    {
        if (Playerhealth < 0)
            Playerhealth = 100;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("triggerEnter");
        if (other.tag == "Damage")
        {
            // Debug.Log("Hurt!");
            Playerhealth -= 10;
            GetComponent<HurtEffect>().position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            GetComponent<HurtEffect>().Spawn();
            GetComponentInChildren<AudioSource>().Play();
        }
    }

}
