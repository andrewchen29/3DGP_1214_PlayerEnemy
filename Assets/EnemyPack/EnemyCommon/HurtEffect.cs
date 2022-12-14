using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEffect : MonoBehaviour
{
    public Vector3 position;
    public GameObject effect;

    public void Spawn()
    {
        GameObject newEffect = Instantiate(effect);
        newEffect.transform.position = position;
    }
}
