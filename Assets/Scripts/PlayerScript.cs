using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector2 movementInput;
    public float speed;
    CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float XInput = Input.GetAxis("Horizontal");
        float YInput = Input.GetAxis("Vertical");
        movementInput = new Vector2(XInput, YInput);
        movementInput.Normalize();
        controller.Move(movementInput * Time.deltaTime * speed);

    }
}
