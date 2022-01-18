using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movements Variables")]
    [SerializeField] private float runAcceleration; 
    [SerializeField] private float maxRunSpeed; 
    [SerializeField] private float linearDrag = 10; 
    [SerializeField] private float airLinearDrag = 2.5f; 
    private bool changingDirection => (rb.velocity.x > 0f && moveInputX < 0f) || (rb.velocity.x < 0f && moveInputX > 0f); 

    private Rigidbody2D rb;

    private float moveInputX; 
    private float moveInputY; 

    public static bool facingRight = true;

    [Header("CheckGround")]
    private bool isGrounded; 
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float checkRadius; 
    [SerializeField] private LayerMask whatIsGround; 

    [Header("Jump Variables")]
    [SerializeField] private float jumpForce; //Stocker la force du saut
    [SerializeField] private float fallMultiplier = 3f; 
    [SerializeField] private float lowJumpMultiplier = 2.5f;
    private float jumpTime; //Stock le temps de saut restant
    [SerializeField] private float jumpTimeInitial; //Stock le temps de saut maximal
    private bool isJumping; //Stock si le joueur est en train de sauter ou non

    [Header("Dash Variables")]
    [SerializeField] private float dashVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCoolDown = 0.5f;
    private Vector2 dashingDir;
    private bool isDashing;
    private bool canDash = true;
    private bool timeFinished = true;

    [Header("CheckWall")]
    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    [Header("Wall jump")]
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Crouch Variables")]
    public SpriteRenderer spriteRenderer;
    public Sprite standing;
    public Sprite crouching;
    public BoxCollider2D collider;
    public Vector2 standingSize;
    public Vector2 crouchingSize;
    private bool isCrouching;
    public float crouchingSpeed;
    public float crouchingSpeedMax;

    [Header("Swim Variables")]
    public static bool isSwimming;
    public float swimmingGravity;
    public float swimAcceleration;
    public float swimMaxSpeed;

    [Header("CheckWaterHead")]
    private bool isSubmerged; 
    [SerializeField] private Transform headCheck; 
    [SerializeField] private LayerMask whatIsWater; 

    [Header("Malus")]
    [SerializeField] bool inverser = false;
    [SerializeField] float timerInverser;

    public ParticleSystem dust;
    
    void CreateDust(){
        dust.Play();
    }

    private void MoveCharacter()
    {
        rb.AddForce(new Vector2(moveInputX, 0f) * runAcceleration);

        if (Mathf.Abs(rb.velocity.x) > maxRunSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxRunSpeed, rb.velocity.y);
        }
    }

    private void MoveCharacterCrouching()
    {
        rb.AddForce(new Vector2(moveInputX, 0f) * crouchingSpeed);

        if (Mathf.Abs(rb.velocity.x) > crouchingSpeedMax)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * crouchingSpeedMax, rb.velocity.y);
        }
    }

    private void MoveCharacterSwimming()
    {
        rb.AddForce(new Vector2(moveInputX, moveInputY) * swimAcceleration);

        if (Mathf.Abs(rb.velocity.x) > swimMaxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * swimMaxSpeed, rb.velocity.y);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water") == true)
        {
            isSwimming = false;
            rb.gravityScale = 1f;
        }
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void Flip()
    {
        if (isGrounded && !isSwimming) //Si le personnage est au sol
        {
            CreateDust();
        }
        facingRight = !facingRight; 
        transform.Rotate(0f, 180f, 0f); 
    }

    private void ApplyLinearDrag()
    {
        if (Mathf.Abs(moveInputX) < 0.4f || changingDirection)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        rb.drag = airLinearDrag;
    }

    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !isJumping)
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    IEnumerator Dash()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        timeFinished = true;
    }

    IEnumerator InverseControl(){
        inverser = true;
        yield return new WaitForSeconds(timerInverser);
        inverser = false;
    }

    void MoveCharacterSwimmingInverse(){
        rb.AddForce(new Vector2(-moveInputX, moveInputY) * swimAcceleration);

        if (Mathf.Abs(rb.velocity.x) > swimMaxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * swimMaxSpeed, rb.velocity.y);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        jumpTime = jumpTimeInitial;
        spriteRenderer = GetComponent<SpriteRenderer>();
        //spriteRenderer.sprite = standing;
        //standingSize = collider.size;
    }

    // Update is called once per frame
    void Update()
    {
        moveInputX = Input.GetAxisRaw("Horizontal"); //On chope le mouvement horizontal (gauche(-1), droite (1) ou rien (0))
        moveInputY = Input.GetAxisRaw("Vertical"); //On chope le mouvement vertical (bas(-1), haut (1) ou rien (0))

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
        isSubmerged = Physics2D.OverlapCircle(headCheck.position, checkRadius, whatIsWater);

        if (moveInputX < 0 && facingRight && !inverser) //Si on va vers la gauche et qu'on regarde vers la droite
        {
            Flip(); //on flip le personnage
        }
        else if (moveInputX > 0 && !facingRight && !inverser) //si on va vers la droite et qu'on regarde vers la gauche
        {
            Flip(); // on flip le personnage
        }
        else if(moveInputX < 0 && !facingRight && inverser){
            Flip();
        }
        else if (moveInputX > 0 && facingRight && inverser) 
        {
            Flip(); // on flip le personnage
        }

        if(isSubmerged)
        {
            isSwimming = true;
            rb.gravityScale = swimmingGravity;
        }

        // JUMP
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isSwimming && !isCrouching) //Si on appuie sur espace et qu'on est au sol
        {
            Jump(); //On saute
            CreateDust();
            jumpTime = jumpTimeInitial; //On initialise un timer pour le saut
            isJumping = true; //On dit qu'il est en train de sauter
        }

        if (Input.GetKey(KeyCode.Space) && isJumping && !isSwimming && !isCrouching) //Si on continue d'appuyer sur espace et qu'on est en saut
        {
            if (jumpTime > 0) //s'il reste du timer 
            {
                Jump(); //on saute
                jumpTime -= Time.deltaTime; 
            }
            else //sinon
            {
                isJumping = false; 
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            isJumping = false; 
        }

        //DASH
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && canDash && !isSwimming && !isCrouching) //si on appuie sur LeftShift, qu'on est pas en train de dash 
        {
            timeFinished = false;
            canDash = false;
            isDashing = true;
            dashingDir = new Vector2(moveInputX, moveInputY);
            if(dashingDir == Vector2.zero)
            {
                if (facingRight)
                {
                    dashingDir = Vector2.right;
                }
                else
                {
                    dashingDir = Vector2.left;
                }
            }
            //CreateDust(); 
            StartCoroutine(Dash());
        }

        if(!isDashing && timeFinished)
        {
            canDash = true;
        }

        //Wall jump et wall slide
        if(isTouchingFront && !isGrounded && moveInputX != 0  && !isSwimming && !isCrouching) 
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallSliding  && !isSwimming && !isCrouching)
        {
            wallJumping = true;


            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }

        if (Input.GetKeyDown(KeyCode.S) && isGrounded  && !isSwimming)
        {
            //spriteRenderer.sprite = crouching;
            collider.size = crouchingSize;
            isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.S) && isGrounded  && !isSwimming)
        {
            //spriteRenderer.sprite = standing;
            collider.size = standingSize;
            isCrouching = false;
        }
    }

    void FixedUpdate(){
        if(!isDashing && !isSwimming && !isCrouching)
        {
            MoveCharacter(); //On avance en fonction des mouvements

            if (isGrounded)
            {
                ApplyLinearDrag();
            }
            else
            {
                ApplyAirLinearDrag(); 
                FallMultiplier();
            }
        }
        else if (isDashing && !isSwimming && !isCrouching)
        {
            rb.velocity = dashingDir.normalized * dashVelocity;
        }
        else if (isSwimming && !isCrouching && !isDashing)
        {
            if(!inverser){
                MoveCharacterSwimming();
            }
            else{
                MoveCharacterSwimmingInverse();
            }
        }
        else if (isCrouching && !isSwimming && !isDashing){
            MoveCharacterCrouching();
        }

        if (wallSliding && !isSwimming && !isCrouching)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if (wallJumping && !isSwimming && !isCrouching)
        {
            rb.velocity = new Vector2(xWallForce * -moveInputX, yWallForce);
            CreateDust();
        }
    }

}
