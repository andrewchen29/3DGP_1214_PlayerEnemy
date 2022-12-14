using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform crosshair;

    [Range(50f, 250)]
    public float size;

    void Start()
    {
        crosshair = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        crosshair.sizeDelta = new Vector2(size, size);
    }
}
