using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//I'm trying to use this via new input system
using UnityEngine.InputSystem; 
//should allow functions in here to be called via the inputmanager,
//but it doesn't seem to recognize any of the UnityEngine extensions at the moment.
//I'm not sure where in Unity's UI I can drop in assets for public variables

public class Shootbehavior : MonoBehaviour
{
    
    public GameObject bulletPrefab;
    public Rigidbody2D Barrel;
    public float projectileSpeed;
    public InputAction ShootControls;

    private void OnEnable()
    {
        ShootControls.Enable();

    }
    private void OnDisable()
    {
        ShootControls.Disable();
    }


    //Should be called with an input of (InputAction.CallbackContext context) but that's throwing errors at the moment
    public void Fire(InputAction.CallbackContext context)
    {
        //Shoots via instantiating a  bullet prefab and assigning its velocity via its 2D rigidbody
        Debug.Log("Pew Pew");
        Instantiate(bulletPrefab,Barrel.position,Quaternion.identity);
        bulletPrefab.GetComponent<Rigidbody2D>().velocity = transform.forward * projectileSpeed;
    }
}
