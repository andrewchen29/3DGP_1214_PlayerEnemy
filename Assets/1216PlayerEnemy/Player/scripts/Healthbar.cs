using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image healthbar;
    // public Transform Player_t;

    private void Update()
    {
        healthbar.fillAmount = (100 - PlayerState.Playerhealth) / 100;
        // transform.position = Player_t.position + new Vector3(0, 4, 0);
        // transform.rotation = Player_t.rotation;
        // Quaternion rotationAmount = Quaternion.Euler(0, 90, 0);
        // Quaternion postRotation = Player_t.rotation * rotationAmount;
        // transform.rotation = postRotation;
    }
}
