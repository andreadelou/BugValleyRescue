using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private CharacterController characterController;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movementVector = new Vector3(moveX, 0, moveZ);

        if (movementVector.magnitude != 0)
        {
            transform.forward = movementVector;
            characterController.SimpleMove(movementVector * speed);
            animator.SetFloat("Speed", movementVector.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("PickUp");
        }
    }
}