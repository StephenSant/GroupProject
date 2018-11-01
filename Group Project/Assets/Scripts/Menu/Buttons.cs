using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject mainMenu, optionMenu;
    public bool optionOn;

    public GameObject pauseMenu, pauseOptions, gamePanel;
    public bool paused;

    private void Start()
    {
        if (GameObject.Find("Main Menu") != null)
        {
            mainMenu = GameObject.Find("Main Menu");
            optionMenu = GameObject.Find("Option Menu");
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
            optionOn = false;
        }
        else if (GameObject.Find("Pause Menu") != null)
        {
            pauseMenu = GameObject.Find("Pause Menu");
            pauseOptions = GameObject.Find("Pause Options");
            gamePanel = GameObject.Find("Game Panel");
            gamePanel.SetActive(true);
            pauseMenu.SetActive(false);
            pauseOptions.SetActive(false);
            paused = false;
        }
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

    public void TogglePause()
    {
        if (!paused)
        {
            gamePanel.SetActive(false);
            pauseMenu.SetActive(true);
            paused = true;
        }
        else if (paused)
        {
            gamePanel.SetActive(true);
            pauseMenu.SetActive(false);
            paused = false;
        }
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

}
