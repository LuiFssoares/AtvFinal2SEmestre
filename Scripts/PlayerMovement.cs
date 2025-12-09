using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed = 5f;

    [SerializeField] private float jumpForce = 15f;

    public PlayerMovement(float jumpForce)
    {
        this.jumpForce = jumpForce;
    }

    public PlayerMovement(bool isJump)
    {
        IsJump = isJump;
    }

    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Animator animplayer;
    private Collider2D playerCollider;

    public bool IsJump { get; set; }
    [field: SerializeField]
    public bool Dead { get; set; } = false;

    private void Awake()
    {
        animplayer = GetComponent<Animator>();
        rbPlayer = GetComponent<Rigidbody2D>();
        
        playerCollider = GetComponent<Collider2D>();
    }

//Animação de Morte// 
    private void Start()
    {
        if (Dead == false)
        {
            Dead = false;
        }
    }

    [System.Obsolete]
    private void Update()
    {
        if (Dead) return;
        
        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue);

        animplayer.SetBool("Jump", !inFloor);

        if (Input.GetButtonDown("Jump") && inFloor)
            IsJump = true;
        else if(Input.GetButtonUp("Jump")&& rbPlayer.velocity.y > 0)
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, rbPlayer.velocity.y * 0.5f);
            
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        Move();
        JumpPlayer();
    }

    void Move()
    {
        if (Dead) return;
        
        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed, rbPlayer.linearVelocity.y);

        animplayer.SetFloat("Speed", Mathf.Abs(xMove));

        if (xMove > 0)
        {
             transform.eulerAngles = new Vector2(0,0); 
        }
        else if (xMove < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    [System.Obsolete]
    void JumpPlayer()
    {
        if (Dead) return;
        
        if (IsJump)
        {
            rbPlayer.velocity = Vector2.up * jumpForce;
            IsJump = false;
        }
    }

    [Obsolete]
    public void Death()
    {
        Coroutine coroutine = StartCoroutine(DeathCorutine());
    }

    [Obsolete]
    IEnumerator DeathCorutine()
    {
        if (!Dead)
        {
            Dead = true;
            animplayer.SetTrigger("Death");
            yield return new WaitForSeconds(0.5f);
            rbPlayer.velocity = Vector2.zero;
            rbPlayer.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            playerCollider.isTrigger = true;
            Invoke("RestartGame", 2.5f);
        }
        yield break;
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Fase1");
    }

    //Animação de Morte// 


}
