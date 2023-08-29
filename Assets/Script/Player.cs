using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rig;
    public Animator anim;
    private Health healthSystem;

    private bool isJumping;
    private bool DoubleJump;
    private bool recovery;
    

    private bool flipX;

    public float recoveryTime;
    public float speed;
    public float JumpForce;

    public int health;

    //sistema de dash
    private bool canDash = true; // posso dar dash
    private bool isDashing; // dash ativo
    public float dashForce; //força
    public float dashingTime; //tempo
    public float dashingCoolDown;

    [SerializeField] TrailRenderer tr;





    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isDashing)
        {
            return;
        }

        Jump();
        AplicarDash();

        
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Move();

        
        
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if (flipX == true && movement > 0)
        {
            if (!isJumping )
            {
                anim.SetInteger("transition", 1);
            }
            Flip();
        }
        if (flipX == false && movement < 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("transition", 1);
            }
            Flip();
        }
        if (movement == 0 && !isJumping )
        {
            anim.SetInteger("transition", 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isJumping = false;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                // anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                isJumping = true;
                DoubleJump = true;
            }
            else if (DoubleJump)
            {
                //anim.SetInteger("transition", 2);
                rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
                DoubleJump = false;
            }
        }
    }

    public void OnHit(int dmg)
    {
        if (!recovery)
        {
            //anim
            health -= dmg;
            healthSystem.health--;

            if (health <= 0)
            {
                recovery = true;
                //anim
              //  GameController.instance.showGameOver();
            }
            else
            {
                StartCoroutine(Recover());
            }
        }
    }

    private IEnumerator Recover()
    {
        recovery = true;
        yield return new WaitForSeconds(recoveryTime);
        recovery = false;
    }

    private void AplicarDash()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rig.gravityScale;
        rig.gravityScale = 0f;
        rig.velocity = new Vector2(transform.localScale.x * dashForce, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rig.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }


    private void Flip()
    {
        flipX = !flipX;
        float x = transform.localScale.x;

        x *= -1;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.y);
    }

}
