using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCommon : MonoBehaviour
{
    public GameObject hurtSoundSource;
    public GameObject hurtEffectSource;
    public GameObject healthBarSource;
    public Transform playerTransform;
    public int MaxHealth;

    GameObject hurtSound;
    GameObject hurtEffect;
    GameObject healthBar;
    // Start is called before the first frame update
    public void Initialize()
    {
        healthBar = Instantiate(healthBarSource);
        healthBar.transform.SetParent(this.transform);
        healthBar.GetComponentInChildren<MonsterHealthbar>().playerTransform = playerTransform;
        healthBar.GetComponentInChildren<MonsterHealthbar>().monsterTransform = this.transform;
        healthBar.GetComponentInChildren<MonsterHealthbar>().SetMaxHealth(MaxHealth);    
        healthBar.GetComponentInChildren<MonsterHealthbar>().SetHealth(MaxHealth);
        hurtSound = Instantiate(hurtSoundSource);
        hurtSound.transform.SetParent(this.transform);
    }

    public bool IsDeath()
    {
        // return GetComponentInChildren<MonsterHealthbar>().IsDeath();
        if (healthBar.GetComponent<MonsterHealthbar>().IsDeath())
            healthBar.SetActive(false);
        return healthBar.GetComponent<MonsterHealthbar>().IsDeath();
    }

    public void GetHurt(int damage)
    {
        healthBar.GetComponentInChildren<MonsterHealthbar>().TakeDamage(1);
        GameObject newEffect = Instantiate(hurtEffectSource);
        newEffect.transform.position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        hurtSound.GetComponentInChildren<AudioSource>().Play();
    }
    
}
