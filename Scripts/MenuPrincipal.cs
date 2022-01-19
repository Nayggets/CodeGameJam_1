using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject options;

    // Start is called before the first frame update
    void Start()
    {
        options.SetActive(false);
    }

    void Update()
    {
        
    }

    public void active()
    {
        options.SetActive(true);
    }

    public void retour()
    {
        options.SetActive(false);
    }

    public void commencer()
    {
        SceneManager.LoadScene(1);
    }
}
