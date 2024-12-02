using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float speed;
    public float lifetime;
    public float distance;
    public int damage;
    public LayerMask whatIsSolid;

    private float facing;

    private void Start()
    {
        facing = GameObject.FindGameObjectWithTag("Player").transform.localScale.x;
        if (facing < 0)
        {
            transform.localScale *= new Vector2(-1, 1);
        }
    }

    private void Update()
    {
        RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitinfo.collider != null) 
        {
            if (hitinfo.collider.CompareTag("Enemy"))
            {
                hitinfo.collider.GetComponent<Enemy>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        if (facing > 0)
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        lifetime -= Time.deltaTime;
        if (lifetime < 0)
            Destroy(gameObject);
    }
}
