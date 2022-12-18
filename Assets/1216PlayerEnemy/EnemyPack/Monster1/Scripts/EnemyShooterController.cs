using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterController : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxHealth = 3;
    public Transform playerTransform;
    public GameObject HealthBar;
    public GameObject fireEffectSource;
    
    public GameObject bullet;
    public Transform shootPosition;
    AnimatorActions actions;
    AINavagation aiNavagation;
    bool isShooting = false;
    bool isDieing = false;
    Coroutine shoot;
    GameObject fireEffect;
    GameObject hb;

    void Start()
    {
        aiNavagation = GetComponent<AINavagation>();
        actions = new AnimatorActions();
        actions.animator = GetComponent<Animator>();
        actions.SetUpHash();

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
        fireEffect.transform.position = shootPosition.position;
        switch (aiNavagation.GetState())
        {
        case EnemyState.Patrolling:
            isShooting = false;
            actions.Walk();
            StopCoroutine(shoot);
            break;
        case EnemyState.Tracing:
            isShooting = false;
            actions.Run();
            StopCoroutine(shoot);
            break;
        case EnemyState.Attacking:
            if(!isShooting)
            {
                shoot = StartCoroutine(StartShoot());
                isShooting = true;
            }
            // actions.Shoot();
            break;
        }

        if (GetComponentInChildren<MonsterHealthbar>().IsDeath())
        {
            actions.Dead();
            isDieing = true;
            StopCoroutine(shoot);
            Destroy(this.gameObject, 1.5f);
            hb.SetActive(false);
        }
    }

    
    IEnumerator StartShoot()
    {
        while (aiNavagation.GetState() == EnemyState.Attacking)
        {
            actions.Shoot();
            ShootBullet();  
            foreach (var particle in fireEffect.GetComponentsInChildren<ParticleSystem>())
                particle.Play();
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(2.0f);
        }
    }

    void ShootBullet()
    {
        GameObject newBullet = Instantiate(bullet);
        newBullet.transform.position = shootPosition.position;
        Vector3 dir = (playerTransform.position - newBullet.transform.position);
        dir.y = 0.0f;
        dir.Normalize();
        newBullet.GetComponent<Bullet>().Direction = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDieing) return;
            // Debug.Log("enter");
        if (other.tag == "PlayerDamage")
        {
            Debug.Log("Player enter");
            GetComponentInChildren<MonsterHealthbar>().TakeDamage(1);
            GetComponent<HurtEffect>().position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            GetComponent<HurtEffect>().Spawn();
            GetComponentInChildren<AudioSource>().Play();
        }
    }

    private class AnimatorActions
    {
        public Animator animator;
        private int speedHash;
        private int shootHash;
        private int deadHash;

        public void SetUpHash()
        {
            speedHash = Animator.StringToHash("Speed");
            shootHash = Animator.StringToHash("Shoot");
            deadHash = Animator.StringToHash("Dead");
        }

        public void Walk()
        {
            animator.SetFloat(speedHash, 0.6f);
        }

        public void Run()
        {
            animator.SetFloat(speedHash, 1.1f);
        }

        public void Shoot()
        {
            animator.SetFloat(speedHash, 0.0f);
            animator.SetTrigger(shootHash);
        }

        public void Dead()
        {
            animator.SetTrigger(deadHash);
        }
    }


}
