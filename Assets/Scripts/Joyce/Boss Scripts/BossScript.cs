using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    // should store information like health
    public float speed = 5f;
    public float speed2 = 8f;

    public float rollSpeed = 3f;
    public float rollSpeed2 = 4f;
    private bool rolling = false;
    public Transform rollTarget;
    [SerializeField] private float rollDamage = 7f;

    public float speedMultiplier = 1f;

    public float trackTimeMin = 2f;
    public float trackTimeMax = 3f;
    private float trackTime;
    public float trackTimeMultiplier = 1f; // in case we want to shorten/ lengthen the time it takes to track

    [SerializeField] private float contactDamageDelay = 0.2f; // used in case the player is currently in contact with the enemy
    [SerializeField] private float contactDamage = 5;
    private bool inContact = false;

    [SerializeField] private Animator anim;

    [SerializeField] private List<GameObject> attacks;
    private GameObject myAttack;


    [SerializeField] private float maxHealth;
    private float health;

    public Slider healthBar;


    public bool stage2 = false;
    public bool dead = false;
    private Player playerScript;

    [SerializeField] private float top;
    [SerializeField] private float bot;
    [SerializeField] private float left;
    [SerializeField] private float right;

    void Start()
    {
        newTrackTime();
        rolling = false;
        inContact = false;
        rollTarget.position = Vector2.zero;
        myAttack = attacks[GameManagerScript.bossRoll - 1];
        speedMultiplier = 1;
        stage2 = false;
        dead = false;

        health = maxHealth;
        playerScript = FindObjectOfType<Player>();

        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    void Update()
    {
        if (transform.position.x < bot)
        {
            transform.position = new Vector3(transform.position.x, bot);
        }

        if (transform.position.y > right)
        {
            transform.position = new Vector3(right, transform.position.y);
        }
        else if (transform.position.y < left)
        {
            transform.position = new Vector3(left, transform.position.y);
        }
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
            SpawnAttack();
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

        if (collision.gameObject.tag == "Bullet")
        {
            health -= playerScript.bulletDamage;
            healthBar.value = health;

            if (0 < health && health < maxHealth/ 2)
            {
                stage2 = true;
                anim.SetBool("Stage2", stage2);
            }
            else if (health <= 0)
            {
                anim.SetBool("Dead", true);
                dead = true;
                GameManagerScript.instance.Win();
                return;
            }
        }

        // do initial damage here
        if (collision.gameObject.tag == "Player")
        {
            if(rolling)
                playerScript.TakeDamage(rollDamage);
            else
                playerScript.TakeDamage(contactDamage);
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
            print("hitting the wall");
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
            yield return new WaitForSeconds(contactDamageDelay);
            playerScript.TakeDamage(contactDamage);
            print("ow");
        }
    }

    public void SpawnAttack()
    {
        GameObject attack = Instantiate(myAttack, transform.position, transform.rotation);
        attack.transform.parent = transform;
    }

    public void EndAttack()
    {
        anim.SetTrigger("Done");
    }
}
