using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class health_bar : MonoBehaviour
{
    public Image vieDispo1;
    public Image vieDispo2;
    public Image vieDispo3;
    public Image vieDispo4;
    public Image vieDispo5;
    public Sprite vie;
    public Sprite vieMorte;
    public Player p;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
            mortVie();
        
    }

    void mortVie()
    {
        if (p.vieActuelle == 4)
        {
            vieDispo5.sprite = vieMorte;
            vieDispo4.sprite = vie;
            vieDispo3.sprite = vie;
            vieDispo2.sprite = vie;
        }
        else if (p.vieActuelle == 3)
        {
            vieDispo4.sprite = vieMorte;
            vieDispo3.sprite = vie;
            vieDispo2.sprite = vie;
        }
        else if (p.vieActuelle == 2)
        {
            vieDispo3.sprite = vieMorte;
            vieDispo2.sprite = vie;
        }
        else if (p.vieActuelle == 1)
        {
            vieDispo2.sprite = vieMorte;
        }
        else if (p.vieActuelle == 0)
        {
            vieDispo1.sprite = vieMorte;
        }
        else if (p.vieActuelle == 5)
        {
            vieDispo5.sprite = vie;
        }
    }
}
