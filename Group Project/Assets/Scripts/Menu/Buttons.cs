using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject mainMenu, optionMenu;
    public bool optionOn;

    private void Start()
    {
        mainMenu = GameObject.Find("Main Menu");
        optionMenu = GameObject.Find("Option Menu");

        mainMenu.SetActive(true);
        optionMenu.SetActive(false);
        optionOn = false;
    }

    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ToggleOption()
    {
        if (!optionOn)
        {
            mainMenu.SetActive(false);
            optionMenu.SetActive(true);
            optionOn = true;
        }
        else if (optionOn)
        {
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
            optionOn = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
