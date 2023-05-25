using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PlayerAttackObject
{
    Transform target;
    SpriteRenderer spriteRenderer;
    [Header("환경 설정")]
    [SerializeField] float dir;
    [SerializeField] bool isEnemy;
    public float speed;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(isEnemy) dir = (target.position.x - transform.position.x < 0) ? -1 : 1;      // 쏘는 오브젝트가 Enemy일 경우에만 플레이어 방향 따지기

        spriteRenderer.flipX = (dir == 1);      // 1이면 오른쪽, -1이면 왼쪽

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
        if(collision.CompareTag("Player") || collision.CompareTag("Door") || collision.CompareTag("Tile")) DestroyBullet();
    }
}
