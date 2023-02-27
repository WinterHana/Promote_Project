using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove; // 행동지표를 결정할 변수

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        Invoke("Think", 5);     // 5초마다 Think 함수 실행
    }
    void Start()
    {
            
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);         // -1이면 왼쪽, 0이면 멈추기, 1이면 오른쪽으로 이동

        Invoke("Think", 5);                     // 5초마다 Think 함수 실행 by 재귀함수
    }
    void move()
    {
        // 한 방향으로 움직임
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        // 플렛폼(발판) 확인
        // Vector2 frontVec = new Vector2(rigid.position.x, rigid.position.y);
        Debug.DrawLine(rigid.position, Vector2.down, new Color(1, 0, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("tiles"));

        if (rayHit.collider == null)
        {
            Debug.Log("넘어진다!");
            nextMove = nextMove * (-1);
            CancelInvoke();
            Invoke("Think", 5);
        }
    }
}
