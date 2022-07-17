using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    [SerializeField] private float damage = 6f;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float speedMultiplier = 1f;

    [SerializeField] private float lifeSpan = 3f; // the total time before bullet autokill

    private GameObject boss;
    private GameObject player;

    [SerializeField] private Transform movetarget;
    private bool launched = false;
    private bool isTracking = false;
    private Player playerScript;


    private void Start()
    {
        boss = GameObject.FindObjectOfType<BossScript>().gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        launched = false;
        playerScript = FindObjectOfType<Player>();
    }
    private void Update()
    {
        if (isTracking && launched)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * speedMultiplier * Time.deltaTime);
        }
        else if (launched)
        {
            transform.position = Vector2.MoveTowards(transform.position, movetarget.position, speed * speedMultiplier * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool destroy = false;

        if(collision.gameObject.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            destroy = true;
        }
        else if(collision.gameObject.tag == "Despawn" && collision.gameObject != boss) // break if it hits a wall
        {
            destroy = true;
        }

        if(destroy && launched)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch( bool tracking = false)
    {
        launched = true;
        isTracking = tracking;
        StartCoroutine(LifeSpan());
    }

    private IEnumerator LifeSpan()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(this.gameObject);
    }
}
