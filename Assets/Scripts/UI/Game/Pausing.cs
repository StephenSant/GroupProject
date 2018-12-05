using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pausing : MonoBehaviour
{
    public int mainMenu = 0;
    private static bool showPause;

    public GUIStyle pauseTextStyle;
    public GUIStyle buttonStyle;

    public static bool TogglePause()
    {
        if (showPause)
        {
            showPause = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            return (false);
        }
        else
        {
            showPause = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return (true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void OnGUI()
    {

        //screen scaling
        Vector2 scr = Vector2.zero;
        if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
        }
        if (showPause)
        {
            //background
            GUI.Box(new Rect(scr.x * 0, scr.y * 0, scr.x* 16.2f, scr.y*9.1f),"");
            GUI.Box(new Rect(scr.x * 5f, scr.y * 1f, scr.x * 6f, scr.y * 2),"Paused",pauseTextStyle);
            if(GUI.Button(new Rect(scr.x *6.5f, scr.y *5f, scr.x*3f, scr.y*1f), "Resume",buttonStyle))
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                showPause = false;
            }
            if (GUI.Button(new Rect(scr.x *6.5f, scr.y *6.2f, scr.x*3f, scr.y*1f), "Exit To Menu", buttonStyle))
            {
                SceneManager.LoadScene(mainMenu);
            }
        }
    }

}
