using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private float Speed = 0.3f;
    public Vector3 Direction;
    public float distance = 0.0f;
    // Update is called once per frame
    void Start()
    {
        distance = 0.0f;
    }
    void Update()
    {
        transform.position += Direction * Speed; 
        distance += Speed;
        if (transform.position.y > 300.0f || distance > 1000.0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Death()
    {
        Destroy(this);
    }
}
