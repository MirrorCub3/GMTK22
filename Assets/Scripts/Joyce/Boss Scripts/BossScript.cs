using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    // should store information like health
    public float speed = 5f;
    public float speed2 = 8f;

    public float rollSpeed = 3f;
    public float rollSpeed2 = 4f;
    private bool rolling = false;
    public Transform rollTarget;

    public float speedMultiplier = 1f;

    public float trackTimeMin = 2f;
    public float trackTimeMax = 3f;
    private float trackTime;
    public float trackTimeMultiplier = 1f; // in case we want to shorten/ lengthen the time it takes to track

    [SerializeField] private float contactDamageDelay = 0.2f; // used in case the player is currently in contact with the enemy
    [SerializeField] private int contactDamage = 5;
    private bool inContact = false;
    private bool canContactDamage = true;

    [SerializeField] private Animator anim;
    void Start()
    {
        newTrackTime();
        rolling = false;
        inContact = false;
        canContactDamage = true;
        rollTarget.position = Vector2.zero;
    }

    void Update()
    {
        
    }

    public void StartTracking()
    {
        StartCoroutine(Tracking());
    }
    private IEnumerator Tracking()
    {
        yield return new WaitForSeconds(trackTime * trackTimeMultiplier);
        nextAttack();
    }

    private void nextAttack() // called at the end of the tracking phase
    {
        int next = Random.Range(1, 3); // upper number is not included in the int range

        if (next == 1) // Shoot
        {
            anim.SetTrigger("Shoot");
        }
        else // Roll
        {
            anim.SetTrigger("Roll");
            rolling = true;
        }

    }

    private void newTrackTime()
    {
        trackTime = Random.Range(trackTimeMin, trackTimeMin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        // do initial damage here
        if (collision.gameObject.tag == "Player")
        {
            // damage the player here
            print("ouch");
            inContact = true;
            StartCoroutine(ContactDamage());
        }
        
        if (collision.gameObject.tag == "Despawn" && rolling) // stop rolling if the boss hits a wall, go back to tracking phase
        {
            anim.SetTrigger("Done");
            print("hit the wall");
            rolling = false;
            rollTarget.localPosition = Vector2.zero;
            newTrackTime();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Despawn" && rolling) // stop rolling if the boss hits a wall, go back to tracking phase
        {
            anim.SetTrigger("Done");
            print("hit the wall");
            rolling = false;
            rollTarget.localPosition = Vector2.zero;
            newTrackTime();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inContact = false;
        }
    }

    private IEnumerator ContactDamage()
    {
        while (inContact)
        {
            canContactDamage = false;
            yield return new WaitForSeconds(contactDamageDelay);
            // damage the player here
            print("ow");
            canContactDamage = true;
        }
    }




}
