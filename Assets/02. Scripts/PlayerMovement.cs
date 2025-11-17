using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 5.0f;

    private float hAxis;
    private float vAxis;

    private Vector3 moveVec;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        HandleInput();
        Move();
        
    }

    private void HandleInput()
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        moveVec = new Vector3(hAxis, 0.0f, vAxis).normalized;
        transform.position += moveVec * moveSpeed * Time.deltaTime;
        Debug.Log(moveVec);

        anim.SetBool("isRun", moveVec != Vector3.zero);

        transform.LookAt(transform.position + moveVec);
    }
}
