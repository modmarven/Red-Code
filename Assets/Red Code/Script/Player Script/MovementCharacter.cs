using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCharacter : MonoBehaviour
{
    private CharacterController characterController;
    private Animator animator;
    public HealthBarManager healthBarManager;
    public GamePlayUI gamePlayUI;

    public Vector3 moveDirection = Vector3.zero;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float gravity = 9.81f;

    // Movement Input
    private float horizontal;
    private float vertical;

    // Health System
    public float playerHealth = 10f;
    private float barFillAmount = 1f;
    private float damage = 0;
    public ParticleSystem healVFX;

    // Jumping
    public bool isGrounded;
    public LayerMask groundMask;
    public Transform ground_check;
    public float ground_distance = 0.4f;
    [SerializeField] private float jumpHeight = 10.0f;





    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        damage = barFillAmount / playerHealth;
        healVFX.Stop();
    }


    void Update()
    {
        CharMovement();
        MoveAnimation();
        Heal();

        isGrounded = Physics.CheckSphere(ground_check.position, ground_distance, groundMask);
        Debug.Log(playerHealth);

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyHand")
        {
            DamageHealthBar();
        }
    }

    private void DamageHealthBar()
    {
        if (playerHealth > 0)
        {
            playerHealth -= 1;
            barFillAmount = barFillAmount - damage;
            healthBarManager.SetAmount(barFillAmount);
        }

        else if (playerHealth == 0)
        {
            animator.SetBool("dead", true);
            GetComponent<CharacterController>().enabled = false;
            GetComponent<Fighter>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            gamePlayUI.GameOver();
        }
    }

    private void Heal()
    {

        if (Input.GetButton("Heal"))
        {
            if (playerHealth == 0 || playerHealth <= 10)
            {
                playerHealth += 1 * Time.deltaTime;
                barFillAmount = barFillAmount + damage * Time.deltaTime;
                healthBarManager.SetAmount(barFillAmount);
            }

            if (playerHealth < 10 || playerHealth == 0)
            {
                healVFX.Play();
            }

            else
            {
                healVFX.Stop();
            }
        }

    }
}
