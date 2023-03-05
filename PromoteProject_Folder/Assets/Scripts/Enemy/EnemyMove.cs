using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public int nextMove; // �ൿ��ǥ�� ������ ����
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 3);     // 3�ʸ��� Think �Լ� ����
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);         // -1�̸� ����, 0�̸� ���߱�, 1�̸� ���������� �̵�
        anim.SetInteger("WalkSpeed", nextMove); // 0�̸� ���� �ִ�, �ƴϸ� ������ �ִ�
        Debug.Log(nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == -1); // nextMove�� 1�̸� ���� �ٲٱ�
        }

        float nextThinkTime = Random.Range(2f, 5f);

        Invoke("Think", nextThinkTime);                     // 5�ʸ��� Think �Լ� ���� by ����Լ�
    }

    void move()
    {
        // �� �������� ������
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // �÷���(����) Ȯ��
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector2.down * 0.4f, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, LayerMask.GetMask("tiles"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == -1);     // nextMove�� 1�̸� ���� �ٲٱ�

        CancelInvoke();
        Invoke("Think", 2);

    }
}
