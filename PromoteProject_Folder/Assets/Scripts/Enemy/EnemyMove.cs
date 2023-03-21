using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : EnemySight
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    protected int nextMove; // 행동지표를 결정할 변수

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        setViewRotateZ(90f);

        Invoke("Think", 3);     // 3초마다 Think 함수 실행
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);                     // -1이면 왼쪽, 0이면 멈추기, 1이면 오른쪽으로 이동
        anim.SetInteger("WalkSpeed", nextMove);             // 0이면 멈춘 애니, 아니면 움직인 애니
        Debug.Log(nextMove);

        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == -1);        // 처음에 오른쪽을 보고 시작, 왼쪽으로 이동 시 전환
            if (nextMove == 1) setViewRotateZ(90f);         // 시야의 방향 전환
            else if(nextMove == -1) setViewRotateZ(-90f);
        }


        float nextThinkTime = Random.Range(2f, 5f);

        Invoke("Think", nextThinkTime);                     // 5초마다 Think 함수 실행 by 재귀함수
    }

    void move()
    {
        // 한 방향으로 움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 플렛폼(발판) 확인
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
        spriteRenderer.flipX = (nextMove == -1);     // nextMove가 -1이면 방향 바꾸기
        reverseViewRotateZ();

        CancelInvoke();
        Invoke("Think", 2);
    }
}
