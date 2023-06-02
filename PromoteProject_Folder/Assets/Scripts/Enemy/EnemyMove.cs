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
    Transform target;

    [Header("각종 움직임 관련 변수")]
    public int nextMove;    // 행동지표를 결정할 변수
    public bool isMove;     // 움직일 수 있는지에 대한 여부
    public bool isAttack;   // 공격할 수 있는지에 대한 여부
    public bool isPursuit;

    [Header("공격 관련 변수")]
    public float distance;
    public LayerMask isLayer;
    public float atkDistance;
    public GameObject bullet;
    public float cooltime;
    float currentTime;
    bool watching;
    float dir;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        isMove = true;
        isPursuit = false;
        isAttack = true;
    }
    private void Start()
    {
        Invoke("Think", 1f);
        currentSpeed = wanderSpeed;
    }

    void FixedUpdate()
    {
        // 움직일 수 있을 때만 움직인다.
        if (isMove) move();

        // 공격할 수 있을 때만 공격한다.
        if (isAttack) attack();
    }

    void Think()
    {
        isMove = true; isAttack = true;                         // 생각 중이면 항상 움직일 수 있어야 한다.
        // 배회 모드일 때만 정상적으로 작동한다.
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        if (!isPursuit) {
            nextMove = Random.Range(-1, 2);                     // -1이면 왼쪽, 0이면 멈추기, 1이면 오른쪽으로 이동
            anim.SetInteger("isWalking", nextMove);             // 0이면 멈춘 애니, 아니면 움직인 애니
            if (nextMove != 0)
            {
                spriteRenderer.flipX = (nextMove == -1);        // 처음에 오른쪽을 보고 시작, 왼쪽으로 이동 시 전환
            }
        }
        float nextThinkTime = Random.Range(1f, 2f);             // 랜덤한 수마다 다시 실행하기

        Invoke("Think", nextThinkTime);
    }

    void move()
    {
        // 추적 모드
        if (isPursuit) {
            // dir = (target.position.x - transform.position.x < 1) ? -1 : 1;
            // 플레이어가 머리 위로 올라갈 때 조절
            if (target.position.x - transform.position.x < -1)
            {
                dir = -1;
                anim.SetBool("isRun", true);
            }
            else if (target.position.x - transform.position.x > 1)
            {
                dir = 1;
                anim.SetBool("isRun", true);
            }
            else {
                dir = 0;
                anim.SetBool("isRun", false);
            }  
            watching = (dir == -1) ? true : false;
            spriteRenderer.flipX = watching;
            rigid.velocity = new Vector2(dir * currentSpeed, rigid.velocity.y);
        }

        // 배회 모드
        else {
            anim.SetBool("isRun", false);
            rigid.velocity = new Vector2(nextMove * currentSpeed, rigid.velocity.y);
        }
        
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


    // 스턴을 거는 함수
    public void stun() {
        // Think 전부 멈추기
        CancelInvoke(); 
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // 움직임 정지
        isMove = false;
        isAttack = false;
        nextMove = 0;

        // 애니매이션 Idle로 바꾸기
        anim.SetBool("isRun", false);
        anim.SetBool("isAttack", false);
        anim.SetInteger("isWalking", nextMove);

        Invoke("Think", 3.0f);
    }

    void attack()
    {
        // 공격 관련 기능
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * dir, distance, isLayer);

        Debug.DrawRay(transform.position, transform.right * dir * distance, new Color(255, 255, 0));

        if (raycast.collider != null)
        {
            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {
                anim.SetBool("isAttack", true);
                isMove = false;
                Debug.Log("거리 확인");
                if (currentTime <= 0)
                {
                    Debug.Log("총 발사");
                    GameObject bulletcopy = Instantiate(bullet);
                    bulletcopy.transform.position = new Vector2(transform.position.x, transform.position.y);
                    currentTime = cooltime;
                }
            }
        }
        else
        {
            isMove = true;
            anim.SetBool("isAttack", false);
        }
        currentTime -= Time.deltaTime;
    }
}

