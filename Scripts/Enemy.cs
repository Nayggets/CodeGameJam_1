using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxLife;
    public float actualHealth;
    private Rigidbody2D rb;
    

    void Awake(){
        actualHealth = maxLife;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Damage(float damage, float position){
        //CreateBlood();
        actualHealth -= damage;
        StartCoroutine(Knockback(0.02f, 350f, transform.position, position));
    }

    public IEnumerator Knockback(float knockDur, float knockBackPwr, Vector3 knockbackDir, float position){
        float timer = 0;

        while (knockDur > timer){
            timer +=Time.deltaTime;
            if(position > transform.position.x){
                rb.AddForce(new Vector3(knockbackDir.x * -100, knockbackDir.y * knockBackPwr, transform.position.z));
            }
            else{
                rb.AddForce(new Vector3(knockbackDir.x * 100, knockbackDir.y * knockBackPwr, transform.position.z));
            }
        }

        yield return 0;
    }

    void Update(){
        if(actualHealth <= 0){
            Destroy(gameObject);
        }
    }
}