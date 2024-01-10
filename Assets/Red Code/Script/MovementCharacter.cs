using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;

    private Vector3 moveDirection = Vector3.zero;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    private float horizontal;
    private float vertical;

    //Jumping

    public bool isGrounded;
    public LayerMask groundMask;
    public Transform ground_check;
    public float ground_distance = 0.4f;
    [SerializeField] private float jumpHeight = 10.0f;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        CharMovement();
        MoveAnimation();

        isGrounded = Physics.CheckSphere(ground_check.position, ground_distance, groundMask);
    }

    private void CharMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (isGrounded)
        {
            moveDirection = new Vector3(horizontal, 0, vertical) * speed;
            moveDirection = transform.TransformDirection(moveDirection);

            CharJumping();
        }
        
        characterController.Move(moveDirection * Time.deltaTime);
        moveDirection.y -= gravity * Time.deltaTime;

    }

    private void MoveAnimation()
    {
        // Character Movement Animation
        animator.SetFloat("Xmove", horizontal);
        animator.SetFloat("Ymove", vertical);
    }

    private void CharJumping()
    {
       if (Input.GetButton("Jump") && isGrounded)

       {
            moveDirection.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            animator.SetBool("jump", true);

       }
        else
        {
            animator.SetBool("jump", false);
        }

    }

}
