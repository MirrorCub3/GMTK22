using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceMachineScript : MonoBehaviour
{
    private bool canRoll = true;
    [SerializeField] private Animator shakeCam;
    [SerializeField] private float rollTime = 0.45f;
    [SerializeField] private float displayTime = 1f;

    // UI stuff
    [SerializeField] private GameObject d1;
    private Animator d1Anim;

    [SerializeField] private GameObject d2;
    private Animator d2Anim;

    [SerializeField] private Text promptText;
    
    [TextArea][SerializeField]
    private string rollPrompt;

    [TextArea][SerializeField]
    private string rollPrompt2;

    [TextArea][SerializeField]
    private string startPrompt;

    [SerializeField] private GameObject closedRoom;
    [SerializeField] private GameObject openRoom;


    private void Start()
    {
        canRoll = true;
        promptText.text = rollPrompt;

        d1Anim = d1.GetComponent<Animator>();
        d1.SetActive(false);

        d2Anim = d2.GetComponent<Animator>();
        d2.SetActive(false);

        closedRoom.SetActive(true);
        openRoom.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject trigObj = collision.gameObject;
        
        if(trigObj.tag == "Player" && canRoll)
        {
            if(trigObj.GetComponent<MovementScript>() != null && trigObj.GetComponent<MovementScript>().isDashing())
            {
                GameManagerScript.instance.Roll();
                StartCoroutine(Rolling());
            }
            else
            {
                promptText.text = rollPrompt2;
                shakeCam.SetTrigger("Shake");
            }
        }
    }

    private IEnumerator Rolling()
    {
        canRoll = false;
        shakeCam.SetBool("Roll", true);
        promptText.text = "";

        d1.SetActive(true);
        d2.SetActive(true);
        yield return new WaitForSeconds(rollTime);

        shakeCam.SetBool("Roll", false);
        promptText.text = startPrompt;
        closedRoom.SetActive(false);
        openRoom.SetActive(true);

        d1Anim.SetInteger("Num", GameManagerScript.playerRoll);
        d2Anim.SetInteger("Num", GameManagerScript.bossRoll);

        yield return new WaitForSeconds(displayTime);

        d1.SetActive(false);
        d2.SetActive(false);

        // stuff for ui popup here
    }
}
