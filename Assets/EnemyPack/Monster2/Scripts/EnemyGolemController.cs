using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolemController : MonoBehaviour
{
    public int MaxHealth = 3;
    public Transform playerTransform;
    public GameObject HealthBar;

    AnimatorActions actions;
    AINavagation aiNavagation;
    bool isAttacking = false;
    bool isDieing = false;

    void Start()
    {
        aiNavagation = GetComponent<AINavagation>();
        actions = new AnimatorActions();
        actions.animator = GetComponent<Animator>();
        actions.SetUpHash();

        var hb = Instantiate(HealthBar);
        hb.transform.SetParent(this.transform);
        GetComponentInChildren<MonsterHealthbar>().playerTransform = playerTransform;
        GetComponentInChildren<MonsterHealthbar>().monsterTransform = this.transform;
        GetComponentInChildren<MonsterHealthbar>().SetMaxHealth(MaxHealth);
        Vector3 old = GetComponentInChildren<MonsterHealthbar>().transform.position;
        GetComponentInChildren<MonsterHealthbar>().transform.position = old + new Vector3(0.0f, 2.0f, 0.0f);
        GetComponentInChildren<MonsterHealthbar>().SetHealth(MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDieing) return;
        switch (aiNavagation.GetState())
        {
        case EnemyState.Patrolling:
            isAttacking = false;
            actions.Walk();
            break;
        case EnemyState.Tracing:
            actions.Walk();
            isAttacking = false;
            break;
        case EnemyState.Attacking:
            actions.Attack();
            break;
        }
        if (GetComponentInChildren<MonsterHealthbar>().IsDeath())
        {
            actions.Dead();
            isDieing = true;
            Destroy(this.gameObject, 2.0f);
        }
    }

    private class AnimatorActions
    {
        public Animator animator;
        private int speedHash;
        private int attackHash;
        private int deadHash;
        private int getHitHash;

        public void SetUpHash()
        {
            speedHash = Animator.StringToHash("Speed");
            attackHash = Animator.StringToHash("Attack");
            deadHash = Animator.StringToHash("Dead");
            getHitHash = Animator.StringToHash("GetHit");
        }

        public void Walk()
        {
            animator.SetFloat(speedHash, 1.0f);
        }

        public void Attack()
        {
            animator.SetFloat(speedHash, 0.0f);
            animator.SetTrigger(attackHash);
        }

        public void Dead()
        {
            animator.SetTrigger(deadHash);
        }

        public void GetHit()
        {
            animator.SetTrigger(getHitHash);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDieing) return;
        if (other.tag == "PlayerDamage")
        {
            actions.GetHit();
            GetComponentInChildren<MonsterHealthbar>().TakeDamage(1);
            GetComponent<HurtEffect>().position = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            GetComponent<HurtEffect>().Spawn();
            GetComponentInChildren<AudioSource>().Play();
        }
    }
}
