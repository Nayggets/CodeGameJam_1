using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oursin : MonoBehaviour
{

    public PlayerCombatController PCC2;
    private CircleCollider2D Collider;
    
    public Transform transform;

    public LayerMask layerMask;

    public float Range , RangeAttack;

    private bool HasTrue;

    void Start()
    {
        Collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, Range, layerMask) == true ){
            if(HasTrue == false)
            {
                transform.localScale = new Vector3(5, 5, 1);
                HasTrue = true;
            }
            
        }
        else{
            Collider.radius = 0.1f;
            transform.localScale = new Vector3(1, 1, 1);
            if(HasTrue == true)
            {
                Collider.radius -=5f;
                HasTrue = false;
            }
        }
        if(Physics2D.OverlapCircle(transform.position , RangeAttack, layerMask))
        {
            PCC2.TakeDamage(1, transform.position.x);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.DrawWireSphere(transform.position, RangeAttack);
    }
}
