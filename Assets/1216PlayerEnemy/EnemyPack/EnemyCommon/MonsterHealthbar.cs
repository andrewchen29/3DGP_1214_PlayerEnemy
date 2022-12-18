using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthbar : MonoBehaviour
{
    public Image healthbar;
    public Transform playerTransform;
    public Transform monsterTransform;
    int maxHealth = 2;
    int monsterhealth = 2;

    private void Update()
    {
        float fa = (float)(maxHealth - monsterhealth) / (float)maxHealth;
        healthbar.fillAmount = fa;
        transform.position = monsterTransform.position + new Vector3(0, 3, 0);
        transform.LookAt(Camera.main.transform);
    }

    public void SetHealth(int value)
    {
        monsterhealth = value;
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
    }

    public bool IsDeath()
    {
        return monsterhealth <= 0;
    }

    public int GetHealth()
    {
        return monsterhealth;
    }

    public void TakeDamage(int value)
    {
        monsterhealth -= value;
    }
}
