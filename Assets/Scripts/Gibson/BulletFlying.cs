using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlying : MonoBehaviour
{
    [SerializeField] private float destroyDelay; // length of collision animation
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Despawn") // Destroys the bullet if it hits anything tagged Despawn - this way we can tag the boss with it too
        {
            StartCoroutine(DestroyBullet());
        }
    }

    private IEnumerator DestroyBullet()
    {
        // start collision animation
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Collide");
        yield return new WaitForSeconds(destroyDelay);
        Destroy(this.gameObject);
    }
}
