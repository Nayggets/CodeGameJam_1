using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool enPause;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enPause)
            {
                resume();
            }
            else
            {
                pause();
            }
        }
    }

    void pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        enPause = true;
    }

    void resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        enPause = false;
    }


}
