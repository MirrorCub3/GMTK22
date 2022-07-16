using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    // movement variables
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashDis = 2;

    [SerializeField] private Rigidbody2D rb;
    private Vector2 movement;

    [SerializeField] private Animator anim;

    //dashing variables
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashTime = 2f;
    [SerializeField] private float dashCooldown = 5f;
    private bool dashing = false;
    private bool canDash = true;
    [SerializeField] private TrailRenderer trail;

    private enum Dir { LEFT, RIGHT, UP, DOWN}
    private Dir playerDir = Dir.UP;

    private void Start()
    {
        dashing = false;
        canDash = true;
        playerDir = Dir.UP;
    }

    void Update()
    {
        if (dashing)
        {
            return;
        }

        // resets to up if not given a direction input
        playerDir = Dir.UP;

       // priority on x movement // also keeps track of direction to dash
        movement.x = Input.GetAxisRaw("Horizontal");
        if (movement.x == 0)
        {
            movement.y = Input.GetAxisRaw("Vertical");
            if(movement.y > 0)
            {
                playerDir = Dir.UP;
            }
            else if (movement.y < 0)
            {
                playerDir = Dir.DOWN;
            }
        }
        else
        {
            movement.y = 0;
            if (movement.x > 0)
            {
                playerDir = Dir.RIGHT;
            }
            else if (movement.x < 0)
            {
                playerDir = Dir.LEFT;
            }
        }


        // controls the animator
        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());
        }
        
    }

    private void FixedUpdate()
    {
        if (dashing)
        {
            return;
        }
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        dashing = true;
        switch (playerDir)
        {
            case Dir.LEFT:
                rb.velocity = new Vector2(-dashForce, 0f);
                break;
            case Dir.RIGHT:
                rb.velocity = new Vector2(dashForce, 0f);
                break;
            case Dir.DOWN:
                rb.velocity = new Vector2(0f, -dashForce);
                break;
            default:
                rb.velocity = new Vector2(0f, dashForce);
                break;
        }

        trail.emitting = true;
        yield return new WaitForSeconds(dashTime);
        dashing = false;

        trail.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

    }
}
