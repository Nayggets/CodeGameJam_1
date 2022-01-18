using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class oxygene : MonoBehaviour
{
    [SerializeField] Image barre;
    [SerializeField] [Range(0, 1)] float barreVal;
    public bool dansEau;

    // Start is called before the first frame update
    void Start()
    {
        barreVal = 1;
    }

    // Update is called once per frame
    void Update()
    {
        barre.fillAmount = barreVal;
    }

    private void FixedUpdate()
    {
        if (dansEau&&barreVal>0)
        {
            barreVal -= 0.001f;
        }
        else if (!dansEau && barreVal < 1)
        {
            barreVal += 0.01f;
            if (barreVal > 1f)
            {
                barreVal = 1f;
            }
        }
    }

}

