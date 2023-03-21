using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemySight
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    protected int nextMove; // �ൿ��ǥ�� ������ ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        setViewRotateZ(90f);

        Invoke("Think", 3);     // 3�ʸ��� Think �Լ� ����
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);                     // -1�̸� ����, 0�̸� ���߱�, 1�̸� ���������� �̵�
        anim.SetInteger("WalkSpeed", nextMove);             // 0�̸� ���� �ִ�, �ƴϸ� ������ �ִ�
        Debug.Log(nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == -1);        // ó���� �������� ���� ����, �������� �̵� �� ��ȯ
            if (nextMove == 1) setViewRotateZ(90f);         // �þ��� ���� ��ȯ
            else if(nextMove == -1) setViewRotateZ(-90f);
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
        Debug.DrawRay(frontVec, Vector2.down * 1.0f, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, LayerMask.GetMask("tiles"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == -1);     // nextMove�� -1�̸� ���� �ٲٱ�
        reverseViewRotateZ();

        CancelInvoke();
        Invoke("Think", 2);
    }
}
