using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; // 행동지표를 결정할 변수
    SpriteRenderer spriteRenderer;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 3);     // 3초마다 Think 함수 실행
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);         // -1이면 왼쪽, 0이면 멈추기, 1이면 오른쪽으로 이동

        if(nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == 1); // nextMove가 1이면 방향 바꾸기
        }

        float nextThinkTime = Random.Range(2f, 5f);

        Invoke("Think", nextThinkTime);                     // 5초마다 Think 함수 실행 by 재귀함수
    }

    void move()
    {
        // 한 방향으로 움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 플렛폼(발판) 확인
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawLine(frontVec, Vector2.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("tiles"));

        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void Turn()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == 1);     // nextMove가 1이면 방향 바꾸기

        CancelInvoke();
        Invoke("Think", 2);

    }
}
