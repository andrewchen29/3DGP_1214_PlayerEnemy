using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RobotController : MonoBehaviour
{
    public Rigidbody robot_rb;
    public float rSpeed = 1f;
    public Animator anim;
    public Transform groundCheck;
    public float jumpHeight = 2f;
    public LayerMask whatGround;
    public Transform cam;


    private float hInput;
    private float vInput;
    private CharacterController controller;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, whatGround);
    }

    private void FixedUpdate()
    {
        Vector3 moveV = cam.forward * Input.GetAxis("Vertical");
        Vector3 moveH = cam.right * Input.GetAxis("Horizontal");
        // Vector3 move = (moveH + moveV) * rSpeed;
        // move = move - new Vector3(0, move.y, 0);

        // Vector3 move = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal") * -1f) * rSpeed;

        Vector3 move = transform.forward * -1f * Input.GetAxis("Horizontal") + transform.right * Input.GetAxis("Vertical");
        move = move * rSpeed;

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Debug.Log("jump");
            robot_rb.AddForce(jumpHeight * Vector3.up, ForceMode.VelocityChange);
            isGrounded = false;
        }


        if (move != Vector3.zero)
        {
            Debug.Log("GO!");
            robot_rb.velocity = move + new Vector3(0, robot_rb.velocity.y, 0);
            anim.SetBool("isRunning", true);
            robot_rb.transform.right = robot_rb.velocity - new Vector3(0, robot_rb.velocity.y, 0);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetAxis("Horizontal") > 0) anim.SetBool("runLeft", true);
        else anim.SetBool("runLeft", false);

        if (Input.GetAxis("Horizontal") < 0) anim.SetBool("runRight", true);
        else
        {
            anim.SetBool("runRight", false);   
        }
    }
}
