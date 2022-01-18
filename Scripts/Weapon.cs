using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0) && ChangeWeapon.currentWeaponNumber == 1 && !PlayerController.isSwimming)
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}