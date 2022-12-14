using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIrePoint : MonoBehaviour
{
    [SerializeField] Transform FirePointTransform;

    public Vector3 GetPosition()
    {
        return FirePointTransform.position;
    }
}
