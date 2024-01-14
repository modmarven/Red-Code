using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private CharacterController characterController;
    private float gravity = 9.81f;
    private Vector3 velocity;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    
    void Update()
    {
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        if (characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime; 
        }
        else
        {
            velocity.y = -0.5f;
        }

    }
}
