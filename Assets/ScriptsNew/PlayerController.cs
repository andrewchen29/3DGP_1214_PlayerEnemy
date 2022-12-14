using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _player_rb;
    private Animator _anim;
    
    public Transform GroundCheck;
    public LayerMask WhatGround;

    private Vector3 _move;

    [SerializeField]
    private float _runSpeed = 1f;
    [SerializeField]
    private float _jumpHeight = 6f;

    private bool _isGrounded;

    public Transform CamTarget;

    private void Start()
    {
        _player_rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //_isGrounded = Physics.CheckSphere(GroundCheck.position, 0.1f, WhatGround);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Vector3 forward = Vector3.Normalize(new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(CamTarget.position.x, 0, CamTarget.position.z));
        Vector3 right = Vector3.Cross(Vector3.up, forward);

        Debug.DrawRay(transform.position, forward);
        Debug.DrawRay(transform.position, right);

        Vector3 moveV = forward * Input.GetAxis("Vertical");
        Vector3 moveH = right * Input.GetAxis("Horizontal");
        _move = _runSpeed * (moveH + moveV);

        if (Input.GetKey(KeyCode.Space) && _isGrounded)
        {
            _player_rb.AddForce(_jumpHeight * Vector3.up, ForceMode.VelocityChange);
            _isGrounded = false;
            _anim.SetBool("isJumping", true);
        }
        else
        {
            _anim.SetBool("isJumping", false);
        }

        if (_move != Vector3.zero)
        {
            _player_rb.transform.LookAt(_move + transform.position);
            _player_rb.velocity = _move + new Vector3(0, _player_rb.velocity.y, 0);
            _anim.SetBool("isRunning", true);
        } else
        {
            _anim.SetBool("isRunning", false);
        }

    }

    private void OnCollisionEnter(Collision collisionObject){
        //if(collisionObject.gameObject.tag == "floor"){
            _isGrounded = true;
            print("Ground");
        //}
    }

}
