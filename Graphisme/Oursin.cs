using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oursin : MonoBehaviour
{
    private CircleCollider2D Collider;
    
    public Transform transform;

    public LayerMask layerMask;

    public float Range , RangeAttack;

    void Start()
    {
        Collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, Range, layerMask) == true){
            transform.localScale = new Vector3(5, 5, 1);
        }
        else{
            Collider.radius = 0.1f;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }
}
