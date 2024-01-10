using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    private Rigidbody rBody;

    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] private float speed = 5.0f; 
    [SerializeField] private float gravity = 9.81f;
    
    private float horizontal;
    private float vertical;

    public bool isGrounded;
    public LayerMask groundMask;
    public Transform ground_check;
    public float ground_distance = 0.4f;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rBody = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        CharMovement();
        MoveAnimation();
        CharJump();

        isGrounded = Physics.CheckSphere(ground_check.position, ground_distance, groundMask);
    }

    private void CharMovement()
    {
         horizontal = Input.GetAxis("Horizontal");
         vertical = Input.GetAxis("Vertical");

        isGrounded = characterController.isGrounded;

        if (isGrounded)
        {
            moveDirection = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
            moveDirection = transform.TransformDirection(moveDirection);

        }

        characterController.Move(moveDirection);
        moveDirection.y -= gravity * Time.deltaTime;

    }

    private void CharJump()
    {
        if (Input.GetButton("Jump") && isGrounded)

        {
            rBody.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }
    }

    private void MoveAnimation()
    {
        // Character Movement Animation
        animator.SetFloat("Xmove", horizontal);
        animator.SetFloat("Ymove", vertical);
    }

}
