using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashDis = 2;

    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private Animator anim;

    private Vector3 left = new Vector3(0, 0, 90);
    private Vector3 right = new Vector3(0, 0, -90);
    private Vector3 up = Vector3.zero;
    private Vector3 down = new Vector3(0, 0, 180);

  
    void Update()
    {
       // priority on x movement
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement.y = 0;
        }

        // controls the animator
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetButtonDown("Jump"))
        {
            print("dash?");
        }
        
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
