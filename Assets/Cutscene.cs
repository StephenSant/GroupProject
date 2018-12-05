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
        wait = 5;
    }

    // Update is called once per frame
    void Update()
    {
        wait -= Time.deltaTime;
        if (wait <= 0)
        {
            if (!videoPlayer.isPlaying)
            {
                SceneManager.LoadScene(gameScene);
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) )
            {
                SceneManager.LoadScene(gameScene);
            }
    }
}
