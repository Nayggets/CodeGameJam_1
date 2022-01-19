using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private float timeBtwAttack = 0;
    public float StartTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public float damage;
    private Rigidbody2D rb;

    [Header("Life")]
    [SerializeField] private int maxHealth;
    [SerializeField] private int actualHealth;
    private bool canTakeDamage = true;

    public ParticleSystem blood;

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.CompareTag("Enemy")){
            TakeDamage(1, col.transform.position.x);
        }
    }

    void CreateBlood()
    {
        blood.Play();
    }

    public void TakeDamage(int damage, float position){
        if(canTakeDamage){
            CreateBlood();
            canTakeDamage = false;
            actualHealth -= damage;
            StartCoroutine(timeTakeDamage());
            StartCoroutine(Knockback(0.02f, 350f, transform.position, position));
        }
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

    private IEnumerator timeTakeDamage(){
        yield return new WaitForSeconds(0.5f);
        canTakeDamage = true;
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        actualHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeBtwAttack <= 0)
        {
            if (Input.GetMouseButton(0) && ChangeWeapon.currentWeaponNumber == 0 && !PlayerController.isSwimming)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies) ;
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().Damage(damage, transform.position.x);
                }
            }
            timeBtwAttack = StartTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}