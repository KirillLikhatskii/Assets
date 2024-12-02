using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpforce = 15f;
    [SerializeField] private float maxspeed = 5f;
    public Animator anim;
    public GameObject pistol;
    public GameObject sword;
    public GameObject SMG;
    public GameObject gunStats;
    public GameObject gunAmmo;
    public GameObject[] HPbar;

    public bool IsGrounded;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Vector2 moveVector;
    private bool FaceRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Reflect()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if ((((rotZ > -90 && rotZ < 0) || (rotZ < 90 && rotZ > 0)) && !FaceRight) || (((rotZ < -90 && rotZ > -179)||(rotZ <179 && rotZ>90)) && FaceRight)) 
        {
            transform.localScale *= new Vector2(-1, 1);
            FaceRight = !FaceRight;
        }
    }

    private void FixedUpdate()
    {
        CheckGround();
        for (int i = 0; i < lives; i++) 
        {
            HPbar[i].SetActive(true);
        }
        for(int i = 5;i > lives; i--)
        {
            HPbar[i-1].SetActive(false);
        }
    }

    private void Update()
    {
        if (lives <= 0)
        {
            Destroy(gameObject);
        }
        Jump();
        Run();
        Reflect();
        if (Input.GetKey("1"))
        {
            sword.SetActive(true);
            pistol.SetActive(false);
            SMG.SetActive(false);
            gunStats.SetActive(false);
        }
        if (Input.GetKey("2"))
        {
            sword.SetActive(false);
            SMG.SetActive(false);   
            pistol.SetActive(true);
            gunStats.SetActive(true);
            gunAmmo.GetComponent<TextMesh>().text = "/ 6";
        }
        if (Input.GetKey("3"))
        {
            sword.SetActive(false);
            pistol.SetActive(false);
            SMG.SetActive(true);
            gunStats.SetActive(true);
            gunAmmo.GetComponent<TextMesh>().text = "/30";
        }
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));
    }

    private void Run()
    {
        if (Input.GetButton("Horizontal"))
        {
            moveVector.x = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(rb.velocity.x + (moveVector.x * speed), rb.velocity.y);
            if (rb.velocity.x > maxspeed)
               rb.velocity = new Vector2(maxspeed, rb.velocity.y);
            if (rb.velocity.x < -maxspeed)
               rb.velocity = new Vector2(-maxspeed, rb.velocity.y);
        }
        else 
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        
    }


    private void Jump()
    {
        if (IsGrounded && Input.GetButton("Jump"))
            rb.velocity = new Vector2 (rb.velocity.x, jumpforce);
        if (IsGrounded && !Input.GetButton("Jump"))
            rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        IsGrounded = collider.Length > 1;
    }

    public void TakeDamage(int damage)
    {
        lives -= damage;
    }
}
