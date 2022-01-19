using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mort : MonoBehaviour
{
    public Player p;
    public oxygene o;
    public GameObject deathMenu;
    /*public Transform trans;*/
    
    // Update is called once per frame
    void Update()
    {
        deathMenu.SetActive(canner());
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        p.vieActuelle = 5;
        o.barreVal = 1f;
        Time.timeScale = 1f;
    }

    bool canner()
    {
        if (p.vieActuelle == 0 || o.barreVal <= 0f /*|| trans.position.y <= -1*/)
        {
            Time.timeScale = 0f;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void quitter()
    {
            SceneManager.LoadScene(0);
    }
}
