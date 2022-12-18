using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testcollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        // collision.gameObject.GetComponent<
        Debug.Log("Collision");
    }

    void OnTriggerEnter(Collider collider)
    {
        // Debug.Log("Trigger");
    }
}
