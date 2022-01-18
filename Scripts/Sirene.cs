using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{

public Vector2 vitesse;
private Rigidbody2D rb;
public Transform transform;

public PlayerController PC;
public GameObject Sirenes;
public CircleCollider2D Collider;
public float Radius;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, Radius))
        {
            rb.velocity = vitesse;
        }
        
        if(Physics2D.OverlapCircle(Sirenes.transform.position, Collider.radius))
        {
            //PC.InverseControl();
            Destroy(Sirenes);
        }
    }
}
