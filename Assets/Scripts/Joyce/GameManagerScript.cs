using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// responsible for the rng number storage and other game management
public class GameManagerScript : MonoBehaviour
{ 
    public static GameManagerScript instance;
    public static int playerRoll = 0;
    public static int bossRoll = 0;

    [SerializeField] private int playerMin = 1;
    [SerializeField] private int playerMax = 6;
    [SerializeField] private int bossMin = 1;
    [SerializeField] private int bossMax = 6;

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
    
    public void RestartRoom()
    {

    }

    public void Reroll() // sends player back to first room
    {
        SceneManager.LoadScene(0);
    }

    public void EnterBoss()
    {
        SceneManager.LoadScene(1);
    }
}
