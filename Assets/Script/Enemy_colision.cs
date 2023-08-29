using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_colision : MonoBehaviour
{
    protected Animator anim;

    private Rigidbody2D rig;
    private Player player;

    public int damage;
    public float speed;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {

        rig.velocity = new Vector2(speed, rig.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //devo criar um objeto vazio e adicionar box collider com istrigger true
        if (collision.gameObject.layer == 8)
        {
            speed = -speed;
            if (transform.eulerAngles.y == 0)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        if (collision.CompareTag("Player"))
        {
            player.OnHit(damage);
        }
    }




}
