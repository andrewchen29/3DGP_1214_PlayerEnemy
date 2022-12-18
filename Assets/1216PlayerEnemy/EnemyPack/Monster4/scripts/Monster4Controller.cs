using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster4Controller : MonoBehaviour
{
    private AINavagation navigation;
    private AnimatorActions actions = new AnimatorActions();
    private EnemyCommon ec;

    bool isDieing = false;
    bool isAttacking = false;
    Coroutine attack;
    // Start is called before the first frame update
    void Start()
    {
        navigation = GetComponent<AINavagation>();
        actions.animator = GetComponent<Animator>();
        actions.SetUpHash();
        ec = GetComponent <EnemyCommon>();
        ec.playerTransform = navigation.playerTransform;
        ec.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDieing) return;
        if (navigation.GetState() != EnemyState.Attacking)
        {
            if (attack != null)
                StopCoroutine(attack);
            isAttacking = false;
        }
        switch (navigation.GetState())
        {
            case EnemyState.Patrolling:
                actions.Walk();
                break;
            case EnemyState.Tracing:
                actions.Run();
                break;
            case EnemyState.Attacking:
                actions.Idle();
                if (!isAttacking)
                    attack = StartCoroutine(StartAttack());
                isAttacking = true;
                break;   
        }
        if (ec.IsDeath())
        {
            actions.Dead();
            Destroy(this.gameObject, 1.5f);
            isDieing = true;
            StopCoroutine(attack);
        }
    }

    IEnumerator StartAttack()
    {
        while (true)
        {
            actions.Attack();
            yield return new WaitForSeconds(1.0f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerDamage")
        {
            actions.GetHit();
            ec.GetHurt(1);
        }
    }

    private class AnimatorActions
    {
        public Animator animator;
        int attackHash;
        int deadHash;
        int getHitHash;
        int speedHash;

        public void SetUpHash()
        {
            attackHash = Animator.StringToHash("Attack");
            deadHash = Animator.StringToHash("Dead");
            getHitHash = Animator.StringToHash("GetHit");
            speedHash = Animator.StringToHash("Speed");
        }

        public void Walk()
        {
            animator.SetFloat(speedHash, 0.6f);
        }

        public void Idle()
        {
            animator.SetFloat(speedHash, 0.0f);
        }

        public void Run()
        {
            animator.SetFloat(speedHash, 1.2f);

        }

        public void GetHit()
        {
            animator.SetTrigger(getHitHash);
        }

        public void Dead()
        {
            animator.SetTrigger(deadHash);

        }
        public void Attack()
        {
            animator.SetTrigger(attackHash);
        }
    }
}
