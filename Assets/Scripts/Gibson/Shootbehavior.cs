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
    //public Camera
    public GameObject bulletPrefab;
    public Transform Barrel;
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
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        float ShotAngle = Mathf.Atan2(mousePos.y - Barrel.position.y, mousePos.x - Barrel.position.x) * Mathf.Rad2Deg;
        Barrel.rotation = Quaternion.Euler(Barrel.position.x, Barrel.position.y, ShotAngle);

        Quaternion aim = Quaternion.FromToRotation(Barrel.position, mousePos);
        GameObject bullet = Instantiate(bulletPrefab, Barrel.position, Barrel.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = Barrel.right * projectileSpeed;
    }
}
