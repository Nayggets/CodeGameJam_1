using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpon : MonoBehaviour
{
    public float offset;

    public Transform playerTransform;
    public PlayerController player;
    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Update()
    {
        if(ChangeWeapon.currentWeaponNumber == 2){
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            if(difference.x > 1 && !PlayerController.facingRight){
                player.Flip();
            }
            else if (difference.x < -1 && PlayerController.facingRight){
                player.Flip();
            }
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Instantiate(projectile, shotPoint.position, transform.rotation);
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

    void Shoot()
    {
        Instantiate(projectile, shotPoint.position, shotPoint.rotation);
    }
}
