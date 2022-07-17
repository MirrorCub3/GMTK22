using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// responsible for the rng number storage and other game management
public class GameManagerScript : MonoBehaviour
{ 
    public static GameManagerScript instance;
    public static int playerRoll = 1;
    public static int bossRoll = 1;

    [SerializeField] private int playerMin = 1;
    [SerializeField] private int playerMax = 6;
    [SerializeField] private int bossMin = 1;
    [SerializeField] private int bossMax = 6;

    private GameObject escMenu;
    public bool paused = false;

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
        paused = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && escMenu != null)
        {
            if (escMenu.activeSelf)
            {
                Time.timeScale = 1;
                escMenu.SetActive(false);
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                escMenu.SetActive(true);
                paused = true;
            }
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
        Time.timeScale = 1;
        paused = false;

        // in case there are any bullets left over on the screen
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }
    }

    public void EnterBoss()
    {
        SceneManager.LoadScene(1);
    }

    public void SetEscMenu(GameObject menu)
    {
        escMenu = menu;
    }
}
