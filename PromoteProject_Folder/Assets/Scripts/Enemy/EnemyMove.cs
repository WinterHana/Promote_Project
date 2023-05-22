using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기본적인 움직임 생성
// 컴포넌트가 없으면 자동으로 추가
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMove : MonoBehaviour
{
    [Header("Speed 설정")]
    public float pursuitSpeed;      // 적이 플레이어를 추적하는 속도
    public float wanderSpeed;       // 평상시의 적 속도
    public float currentSpeed;      // 앞의 둘 중에서 선택할 현재 속도

    [Header("추적 여부 결정")]
    public bool followPlayer;               // 추적할 여부 결정

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public int nextMove; // 행동지표를 결정할 변수
    public bool isMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 1.5f);     // 1.5초마다 Think 함수 실행
        isMove = true;
    }
    private void Start()
    {
        currentSpeed = wanderSpeed;
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);                     // -1이면 왼쪽, 0이면 멈추기, 1이면 오른쪽으로 이동

        anim.SetInteger("isWalking", nextMove);             // 0이면 멈춘 애니, 아니면 움직인 애니
        
        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == -1);        // 처음에 오른쪽을 보고 시작, 왼쪽으로 이동 시 전환
        }

        float nextThinkTime = Random.Range(1f, 2f);         // 랜덤한 수마다 다시 실행하기

        Invoke("Think", nextThinkTime);
    }

    void move()
    {
        // 한 방향으로 움직임
        rigid.velocity = new Vector2(nextMove * currentSpeed, rigid.velocity.y);

        // 플렛폼(발판) 아래 확인
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        RaycastHit2D rayHitDown = Physics2D.Raycast(frontVec, Vector2.down, 1.5f, LayerMask.GetMask("tiles"));
        Debug.DrawRay(frontVec, Vector2.down* 1.5f, new Color(0, 1, 0));

        // 플렛폼(발판) 옆 확인
        RaycastHit2D rayHitFront = Physics2D.Raycast(frontVec, new Vector2(1, 0) * nextMove, 1.0f, LayerMask.GetMask("tiles"));
        Debug.DrawRay(frontVec, new Vector2(1, 0) * nextMove, new Color(0, 1, 0));

        if (rayHitDown.collider == null || rayHitFront.collider != null)
        {
            StartCoroutine(TurnDelay());
        }
    }


    IEnumerator TurnDelay()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == -1);     // nextMove가 -1이면 방향 바꾸기
        CancelInvoke();
        Invoke("Think", 2.0f);
        yield return new WaitForSeconds(0.5f);
    }

    public void stun() {
        CancelInvoke();

        nextMove = 0;
        anim.SetInteger("isWalking", nextMove);

        Invoke("Think", 3.0f);
    }
}

