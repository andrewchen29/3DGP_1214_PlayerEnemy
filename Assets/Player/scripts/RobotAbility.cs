using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAbility : MonoBehaviour
{
    public Transform target;
    public Transform player_t;
    public float autoAttackRange = 0.2f;
    private bool hitEnemy;
    private LayerMask whatEnemy;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 playerRealT = player_t.position + new Vector3(0f, 2f, 0f);
            hitEnemy = Physics.Raycast(playerRealT, target.position - playerRealT, autoAttackRange, whatEnemy);
            Debug.DrawRay(playerRealT, target.position - playerRealT, Color.blue, autoAttackRange);
            anim.SetBool("autoAttack", true);
        }
        else anim.SetBool("autoAttack", false);

    }

}
