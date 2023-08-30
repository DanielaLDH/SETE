using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RayCast : MonoBehaviour
{
    public Transform pointf;
    public Transform pointb;
    private Vector2 direction;
    public float maxVision;
    public float stopDistance;
    public int damage;
    public float speed;

    private Rigidbody2D rig;
    private Player player;

    private bool isDead;
    private bool isFront;
    private bool isRight;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();


        //anim = GetComponent<Animator>();

        if (!isRight)
        {
            transform.eulerAngles = new Vector2(0, 0);
            direction = Vector2.right;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            direction = Vector2.left;
        }
    }


    private void FixedUpdate()
    {
        OnCollisionRaycast();
        onMove();
    }

    void onMove()
    {
        if (isFront && !isDead)
        {
            //anim.SetInteger("transition", 1);

            if (isRight)
            {
                Debug.Log(isRight);
                transform.eulerAngles = new Vector2(0, 180);
                direction = Vector2.right;
                rig.velocity = new Vector2(speed, rig.velocity.y);
            }
            else
            {
                Debug.Log(isRight);
                transform.eulerAngles = new Vector2(0, 0);
                direction = Vector2.left;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }
        }
    }

    void OnCollisionRaycast()
    {
        RaycastHit2D hit = Physics2D.Raycast(pointf.position, direction, maxVision);

        if (hit.collider != null && !isDead)
        {
            if (hit.transform.CompareTag("Player"))
            {
                isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);

                if (distance <= stopDistance)
                {
                    isFront = false;
                    rig.velocity = Vector2.zero;

                   // anim.SetInteger("transition", 2);

                    hit.transform.GetComponent<Player>().OnHit(damage);

                }
            }
            else
            {
                isFront = false;
                rig.velocity = Vector2.zero;
                //anim.SetInteger("transition", 0);
            }
        }
        RaycastHit2D hitbehind = Physics2D.Raycast(pointb.position, -direction, maxVision);

        if (hitbehind.collider != null)
        {
            if (hitbehind.transform.CompareTag("Player"))
            {
                isFront = true;
                isRight = !isRight;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(pointf.position, direction * maxVision);
        Gizmos.DrawRay(pointb.position, -direction * maxVision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision);
            player.OnHit(damage);
        }
    }
}
