using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;
    Position lastPos;

    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {   
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        getLastPosition(movement);
        if(movement.sqrMagnitude < 0.01)
        {
            animator.SetFloat("Position", Convert.ToInt32(lastPos));
        }
       
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void getLastPosition(Vector2 movement)
    {
        
        if(movement.x > 0)
        {
           lastPos = Position.Right;
        }
        if(movement.x < 0)
        {
            lastPos = Position.Left;
        }
        if(movement.y > 0)
        {
            lastPos = Position.Up;
        }
        if(movement.y < 0)
        {
            lastPos = Position.Down;
        }    
    }

    enum Position
    {
        Left,
        Right,
        Up,
        Down
    }

}
