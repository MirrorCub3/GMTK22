using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// responsible for the rng number storage and other game management
public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    public static int playerRoll = 0;
    public static int bossRoll = 1;

    [SerializeField] private int playerMin = 1;
    [SerializeField] private int playerMax = 6;
    [SerializeField] private int bossMin = 1;
    [SerializeField] private int bossMax = 6;

    [SerializeField] private GameObject escMenu;
    [SerializeField] private Animator d1;
    [SerializeField] private Text d1Text;
    [SerializeField] private Animator d2;
    [SerializeField] private Text d2Text;

    public bool paused = false;

    public int playerChances = 3;
    [SerializeField] GameObject endScreen;
    [SerializeField] Text endText;
    
    [SerializeField] private GameObject controls;

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
        playerChances = 3;

        escMenu.SetActive(false);
        endScreen.SetActive(false);

        d1Text.text = "";
        d2Text.text = "";

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && escMenu != null)
        {
            if (escMenu.activeSelf)
            { 
                escMenu.SetActive(false);
                Time.timeScale = 1; 
                paused = false;
                controls.SetActive(false);
            }
            else
            {
                escMenu.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
        }
    }

    public void Roll()
    {
        playerRoll = Random.Range(playerMin, playerMax + 1);
        bossRoll = Random.Range(bossMin, bossMax + 1);

        print(playerRoll + " " + bossRoll);
        d1.SetInteger("Roll Number", playerRoll);

        switch (playerRoll)
        {
            case (1):
                d1Text.text = "x1.25SP";
                break;
            case (2):
                d1Text.text = "x1.5SP";
                break;
            case (3):
                d1Text.text = "+25HP";
                break;
            case (4):
                d1Text.text = "+50HP";
                break;
            case (5):
                d1Text.text = "+2.5ATK";
                break;
            case (6):
                d1Text.text = "+5ATK";
                break;

        }

        d2.SetInteger("Roll Number", bossRoll);
        d2Text.text = "" + bossRoll;
    }
    
    public void RestartRoom()
    {
        playerChances--;
        
        // reload the current room
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);

    }

    public void Reroll() // sends player back to first room
    {
        FindObjectOfType<AudioManager> ().StopPlaying ("Boss Room");

        escMenu.SetActive(false);
        endScreen.SetActive(false);
        d1Text.text = "";
        d2Text.text = "";
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        paused = false;

        // in case there are any bullets left over on the screen
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets) {
            Destroy(bullet);
        }

        playerChances = 3;

        FindObjectOfType<AudioManager> ().StopPlaying ("Yay");
        FindObjectOfType<AudioManager> ().StopPlaying ("Boo");
        FindObjectOfType<AudioManager> ().Play ("Dice Room");
    }
    public void GameOver()
    {
        endScreen.SetActive(true);
        endText.text = "GAME OVER";
        FindObjectOfType<AudioManager> ().StopPlaying ("Boss Room");
        FindObjectOfType<AudioManager> ().Play ("Boo");
    }
    public void Win()
    {
        endScreen.SetActive(true);
        endText.text = "YOU WIN";
        FindObjectOfType<AudioManager> ().StopPlaying ("Boss Room");
        FindObjectOfType<AudioManager> ().Play ("Yay");
    }

    public void EnterBoss()
    {
        SceneManager.LoadScene(1);

        FindObjectOfType<AudioManager> ().Play ("Boss Room");
    }
}