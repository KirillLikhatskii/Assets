using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float maxSpeed;
    public float jumpforce;

    [Header("Атака")]
    public int damage;
    public LayerMask isplayer;
    public Transform attackPos;
    public float radius;

    private GameObject score;
    private Transform player;
    private Rigidbody2D rb;
    private bool FaceRight;
    private bool IsGrounded;
    private float recharge = 0;
    private float startRecharge = 1;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckGround();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player.position.x != transform.position.x || player.position.y != transform.position.y)
        {
            Folow();
        }
        if (health <= 0)
        {
            score = GameObject.FindGameObjectWithTag("Score");
            score.GetComponent<TextMesh>().text = Convert.ToString(Convert.ToInt32(score.GetComponent<TextMesh>().text) + 10);
            Destroy(gameObject);
        }
        if (recharge <= 0)
        {
            recharge = startRecharge;
            Attack();
        }
        else
            recharge -= Time.deltaTime;
    }


    private void Folow()
    {
        int moveVector = 0;
        if (player.position.x - transform.position.x > 0)
            moveVector = 1;
        else
            moveVector = -1;
        Reflect(moveVector);
        if (Math.Abs(player.position.x - transform.position.x) > 1)
        {
            rb.velocity = new Vector2(rb.velocity.x + (moveVector * speed), rb.velocity.y);
            if (rb.velocity.x > maxSpeed)
                rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
            if (rb.velocity.x < -maxSpeed)
                rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        }
        if (player.position.y - transform.position.y -2 > 0)
            Jump();
    }

    void Reflect(int moveVector)
    {
        if ((moveVector<0 && !FaceRight)|| (moveVector > 0 && FaceRight)) 
        {
            transform.localScale *= new Vector2(-1, 1);
            FaceRight = !FaceRight;
        }
    }

    private void Attack()
    {
        Collider2D[] playerCollider = Physics2D.OverlapCircleAll(attackPos.position, radius, isplayer);
        for (int i = 0; i < playerCollider.Length; i++)
        {
            playerCollider[i].GetComponent<Hero>().TakeDamage(damage);
        }
    }

    private void Jump()
    {
        if (IsGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        IsGrounded = collider.Length > 1;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = UnityEngine.Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
}
