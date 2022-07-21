using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private bool isEscMenu = true;
    [SerializeField] private GameObject controls;

    private void Start()
    {
        if (controls)
        {
            controls.SetActive(false);
        }
    }

    public void Reroll()
    {
        PlayClickSound();
        GameManagerScript.instance.Reroll();
    }

    public void OpenControls()
    {
        PlayClickSound();
        controls.SetActive(true);
    }

    public void CloseControls()
    {
        PlayClickSound();
        controls.SetActive(false);
    }

    public void Quit()
    {
        PlayClickSound();
        Application.Quit();
    }

    public void PlayClickSound()
    {
        FindObjectOfType<AudioManager>().Play("Button Click");
    }
}
