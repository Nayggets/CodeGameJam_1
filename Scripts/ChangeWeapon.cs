using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    public static int currentWeaponNumber = 0;

    void Change0(){
        currentWeaponNumber = 0;
        //animator.SetLayerWeight(1, 0);
        //animator.SetLayerWeight(2, 0);
        //animator.SetLayerWeight(0, 1);
    }

    void Change1(){
        currentWeaponNumber = 1;
        //animator.SetLayerWeight(0, 0);
        //animator.SetLayerWeight(1, 1);
        //animator.SetLayerWeight(2, 0);
    }

    void Change2(){
        currentWeaponNumber = 2;
        //animator.SetLayerWeight(0, 0);
        //animator.SetLayerWeight(1, 0);
        //animator.SetLayerWeight(2, 1);
    }

    void Update()
    {
        if((Input.GetKeyDown(KeyCode.Alpha1) && !PlayerController.isSwimming) || (!PlayerController.isSwimming && currentWeaponNumber == 2)){
            Change0();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && !PlayerController.isSwimming){
            Change1();
        }
        if(PlayerController.isSwimming){
            Change2();
        }
    }
}
