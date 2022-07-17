using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private bool isEscMenu = true;
    [SerializeField] private GameObject controls;

    private void Start()
    {
        if (isEscMenu)
        {
            this.gameObject.SetActive(false);
        }
        if (controls)
        {
            controls.SetActive(false);
        }
    }

    public void Reroll()
    {
        GameManagerScript.instance.Reroll();
    }

    public void OpenControls()
    {
        controls.SetActive(true);
    }

    public void CloseControls()
    {
        controls.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
