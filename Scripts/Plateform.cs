using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    [SerializeField] private bool active = true;

    void EnableOrDisable(int element){
        if(element == 0){
            gameObject.SetActive(false);
            active = false;
        }
        else if (element == 1){
            gameObject.SetActive(true);
            active = true;
        }
    }
}
