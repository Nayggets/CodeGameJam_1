using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeduseController : MonoBehaviour
{
    [SerializeField]private float health;
    public float speed;
    int waypointIndex = 0;
    private Collider2D collider;
    [SerializeField] Transform[] waypoints;
    [SerializeField] private float projectileDamage;

    [SerializeField] public ParticleSystem blueBlood;

    void CreateBlueBlood(){
        blueBlood.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
        collider = GetComponent<CircleCollider2D>();
    }

    void Update(){
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public void Damage(float damage){
        CreateBlueBlood();
        health -= damage;
    }

    void Move(){
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].position){
            waypointIndex++;
        }

        if(waypointIndex == waypoints.Length){
            waypointIndex = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Projectile")){
            Damage(projectileDamage);
        }
    }
}
