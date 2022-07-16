using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// responsible for the rng number storage and other game management
public class GameManagerScript : MonoBehaviour
{ 
    private static GameManagerScript instance;
    private static int playerRoll = 0;
    private static int bossRoll = 0;

    [SerializeField] private static int playerMin = 1;
    [SerializeField] private static int playerMax = 4;
    [SerializeField] private static int bossMin = 1;
    [SerializeField] private static int bossMax = 6;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(this);
        }
    }

    public void Roll()
    {
        playerRoll = Random.Range(playerMin, playerMax);
        bossRoll = Random.Range(bossMin, bossMax);

        print(playerRoll + " " + bossRoll);
    }
}
