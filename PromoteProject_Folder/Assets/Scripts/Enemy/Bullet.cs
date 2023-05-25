using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PlayerAttackObject
{
    Transform target;
    SpriteRenderer spriteRenderer;
    [Header("ȯ�� ����")]
    [SerializeField] float dir;
    [SerializeField] bool isEnemy;
    public float speed;

    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(isEnemy) dir = (target.position.x - transform.position.x < 0) ? -1 : 1;      // ��� ������Ʈ�� Enemy�� ��쿡�� �÷��̾� ���� ������

        spriteRenderer.flipX = (dir == 1);      // 1�̸� ������, -1�̸� ����

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
