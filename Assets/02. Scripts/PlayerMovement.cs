using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.25f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded = true;

    private float hAxis;
    private float vAxis;
    private bool jDown;

    private Vector3 moveVec;
    private Animator anim;
    private Rigidbody rb;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleInput();
        

    }

    private void FixedUpdate() 
    {
        UpdateGroundCheck();
    }

    private void UpdateGroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void HandleInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Move()
    {
        moveVec = new Vector3(hAxis, 0.0f, vAxis).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;

        anim.SetBool("isRun", moveVec != Vector3.zero);

        transform.LookAt(transform.position + moveVec);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        anim.SetTrigger("doJump");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(groundCheck.position, groundCheckRadius);
    }

}
