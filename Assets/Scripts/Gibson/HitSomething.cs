using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSomething : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        //There doesn't seem to be a way to get the instance that is colliding with something, so
        //that you destroy that instead of the Prefab with a script on the prefab. Might need to attach this
        //To the walls instead once those have a prefab
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
