using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PlayerAttackObject
{
    public float speed;
    public float distance;
    public LayerMask isLayer;
    Transform target;
    float dir;
    SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        dir = (target.position.x - transform.position.x < 0) ? -1 : 1;
        spriteRenderer.flipX = (dir == 1);
        Invoke("DestroyBullet", 2);
    }


    void Update()
    {
        transform.Translate(transform.right * dir * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.CompareTag("Player")) DestroyBullet();
    }
}
