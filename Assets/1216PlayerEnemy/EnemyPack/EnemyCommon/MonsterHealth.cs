using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    // Start is called before the first frame update
    private int health;

    public void TakeDamage(int value)
    {
        health -= value;
    }

    public void SetHealth(int value)
    {
        health = value;
    }

    public bool IsDeath()
    {
        return health <= 0;
    }

    public int GetHealth()
    {
        return health;
    }
}
