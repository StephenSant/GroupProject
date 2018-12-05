using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscene : MonoBehaviour
{
    public int gameScene;
    public VideoPlayer videoPlayer;
    private float wait;

    // Use this for initialization
    void Start()
    {
        wait = 10;
    }

    // Update is called once per frame
    void Update()
    {
        wait--;
        if (wait <= 0)
        {
            if (Input.GetKeyUp(KeyCode.Space) || !videoPlayer.isPlaying)
            {
                SceneManager.LoadScene(gameScene);
            }
        }
    }
}
