using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScifiSolderAI : MonoBehaviour
{
    public int MaxHealth = 3;
    public Transform playerTransform;
    public GameObject HealthBar;
    public GameObject fireEffectSource;
 
    public GameObject bullet;
    private bool isShooting = false;
    private Monster3Controller controller;
    private Actions solderAction;
    private AINavagation aiNavagation;
    private GameObject fireEffect;
    private EnemyCommon ec;
    private AudioSource fireAudio;
    private Coroutine shoot;
    bool isDieing = false;
    private GameObject hb;
    // Start is called before the first frame update
    void Start()
    {
        solderAction    = GetComponent<Actions>();
        controller      = GetComponent<Monster3Controller>();
        aiNavagation    = GetComponent<AINavagation>();
        ec              = GetComponent<EnemyCommon>();
        fireAudio       = GetComponent<AudioSource>();

        hb = Instantiate(HealthBar);
        hb.transform.SetParent(this.transform);
        GetComponentInChildren<MonsterHealthbar>().playerTransform = playerTransform;
        GetComponentInChildren<MonsterHealthbar>().monsterTransform = this.transform;
        GetComponentInChildren<MonsterHealthbar>().SetMaxHealth(MaxHealth);
        GetComponentInChildren<MonsterHealthbar>().SetHealth(MaxHealth);
        
        fireEffect = Instantiate(fireEffectSource);
        fireEffect.transform.SetParent(this.transform);
    }

    // Update is called once per frame
    void Update()
    { 
        if (isDieing) return;
        fireEffect.transform.position = controller.GetRightGunPosition();
        switch (aiNavagation.GetState())
        {
            case EnemyState.Patrolling:
                solderAction.Walk();
                StopCoroutine(shoot);
                isShooting = false;
                break;    
            case EnemyState.Tracing:
                solderAction.Run();
                StopCoroutine(shoot);
                isShooting = false;
                break;    
            case EnemyState.Attacking:
                Shoot();
                break;    
        }
        if (GetComponentInChildren<MonsterHealthbar>().IsDeath())
        {
            Destroy(this.gameObject, 2.5f);
            if (shoot != null)
                StopCoroutine(shoot);
            solderAction.Death();
            isDieing = true;
            hb.SetActive(false);
        }
    }

    void Shoot()
    {
        solderAction.Aiming();
        if (!isShooting) 
            shoot = StartCoroutine(StartShoot());
        isShooting = true;
    }


    IEnumerator StartShoot()
    {
        while (aiNavagation.GetState() == EnemyState.Attacking)
        // while (isShooting)
        {
            yield return new WaitForSeconds(1.5f);
            solderAction.Attack();
            fireAudio.Play();
            ShootBullet();
        }
    }

    void ShootBullet()
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = controller.GetRightGunPosition();
        foreach (var particle in fireEffect.GetComponentsInChildren<ParticleSystem>())
            particle.Play();
        Vector3 dir = (playerTransform.position - newBullet.transform.position).normalized;
        dir.y = 0.0f;
        newBullet.GetComponent<Bullet>().Direction = dir;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isDieing) return;
        if (other.tag == "PlayerDamage")
        {
            Debug.Log("Player enter");
            solderAction.Damage();
            GetComponentInChildren<MonsterHealthbar>().TakeDamage(1);
            GetComponent<HurtEffect>().position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            GetComponent<HurtEffect>().Spawn();
            GetComponentInChildren<AudioSource>().Play();
        }
    }
}
