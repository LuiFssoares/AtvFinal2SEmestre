using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour
{
Rigidbody2D rbGoomba;
[SerializeField] float speed = 2f;
[SerializeField] Transform point1, point2;
[SerializeField] LayerMask layer;
[SerializeField] bool isCollinding;

Animator animGoomba;
CircleCollider2D colliderGoomba;

private void Awake()
    {
        rbGoomba = GetComponent<Rigidbody2D>();

        animGoomba = GetComponent<Animator>();
        colliderGoomba = GetComponent<CircleCollider2D>();
    }

    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           if(transform.position.y + 0.5f < collision.transform.position.y)
           {
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 6, ForceMode2D.Impulse);
            animGoomba.SetTrigger("Death");
            speed =0;
            colliderGoomba.enabled = false;
            Destroy(gameObject, 0.3f);
           }
           else
           {
           FindObjectOfType<PlayerMovement>().Death();

           Goomba[] goomba = FindObjectsOfType<Goomba>();

           for (int i = 0; i < goomba.Length; i++)
           {
            goomba[i].speed =0;
            goomba[i].animGoomba.speed = 0;
           }
        }
    }

   
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        rbGoomba.velocity = new Vector2(speed, rbGoomba.velocity.y);

        isCollinding = Physics2D.Linecast(point1.position, point2.position, layer);

        if (isCollinding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            speed *= -1;
        }
    }
}
