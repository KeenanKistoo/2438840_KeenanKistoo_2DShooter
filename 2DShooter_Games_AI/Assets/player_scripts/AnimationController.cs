using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;
    private Vector2 movementInput;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Capture player input
        movementInput.x = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right arrow keys
        movementInput.y = Input.GetAxisRaw("Vertical"); // W/S or Up/Down arrow keys

        // Update animator parameters based on movement
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        // Reset all movement animation flags
        /*animator.SetBool("isMovingUp", false);
        animator.SetBool("isMovingDown", false);
        animator.SetBool("isMovingLeft", false);
        animator.SetBool("isMovingRight", false);*/

       
        // Set the appropriate animation flag based on player input
        if (movementInput.y > 0)
        {
            animator.SetBool("isMovingUp", true);
            //print("up");
        }
        else if (movementInput.y < 0)
        {
            animator.SetBool("isMovingDown", true);
            //print("down");
        }
        else if (movementInput.x < 0)
        {
            animator.SetBool("isMovingLeft", true);
            //print("left");
        }
        else if (movementInput.x > 0)
        {
            animator.SetBool("isMovingRight", true);
            //print("right");
        }
    }
}