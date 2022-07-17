using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private MovementScript moveScript;
    private float health;

    public float bulletDamage = 5f;
    void Start()
    {
        health = maxHealth;
        switch (GameManagerScript.playerRoll)
        {
            case (1):
                moveScript.speedMult = 1.25f;
                break;
            case (2):
                moveScript.speedMult = 1.5f;
                break;
            case (3):
                maxHealth += 25;
                health = maxHealth;
                break;
            case (4):
                maxHealth += 50;
                health = maxHealth;
                break;
            case (5):
                bulletDamage += 2.5f;
                break;
            case (6):
                bulletDamage += 5f;
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && GameManagerScript.instance.playerChances > 1)
        {
            GameManagerScript.instance.RestartRoom();
        }
        else if (health <= 0 && GameManagerScript.instance.playerChances <= 0)
        {
            GameManagerScript.instance.GameOver();
        }
    }
}
