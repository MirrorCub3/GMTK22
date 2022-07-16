using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootbehavior : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Vector2 Barrel;
    public float projectileSpeed;

    public void shoot()
    {
        Debug.Log("Pew Pew");
        Instantiate(bulletPrefab,Barrel,Quaternion.identity);
        bulletPrefab.GetComponent<Rigidbody2D>().velocity = transform.forward * projectileSpeed;
    }
}
