using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rigidbody , playerMove 설정 
 * 1. 공기저항(Linear Drag) : 1
 * 2. 중력 비중(Gravity Scale) : 2
 * 3. playerSize = 0.8
 * 4. MaxSpeed = 6
 * 5. JumpPower = 13
 * 6. deceleration = 3.0
 * 7. climbSpeed = 3
 */
public class PlayerMove : MonoBehaviour
{
    const float GRAVITYSCALE = 2.0f;
    [Header("플레이어 물리 상태 설정")]
    [SerializeField] float playerSize;
    [SerializeField] float maxSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float sitSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float rcDistance;
    [SerializeField] LayerMask whatIsLadder;
    [Space]
    [Header("플레이어 움직임 여부")]
    public static bool isMove;      // 많이 쓸 거 같아서 static으로 만듬

    Rigidbody2D rigid;
    Animator ani;
    CapsuleCollider2D standCol;
    BoxCollider2D boxCol;
    GameObject ground;
    
    bool isJump;            // 점프 상태인지 확인
    bool isSit;             // 앉은 상태인지 확인
    bool isLadder;          // 사다리 상태인지 확인
    bool findLadder;        // 레이케스트가 사다리를 찾았음
    float inputHorizontal;
    float inputVertical;

    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        standCol = GetComponent<CapsuleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();
        ground = transform.GetChild(0).gameObject;
        // 오브젝트가 구르는 현상 방지
        rigid.freezeRotation = true;

        // 각종 모션들의 상태
        isJump = false;
        isSit = false;
        isLadder = false;
        findLadder = false;

        // 콜라이더가 2개인데, 서 있을 때는 캡슐, 앉을 때는 박스로 한다.
        standCol.enabled = true;
        boxCol.enabled = false;

        walkSpeed = maxSpeed;

        isMove = true;
    }
    
    void Update()
    {
        if (isMove) 
        {
            jump();
            sit();
            ladder();
            // 키보드에서 손을 땠을 때 완전 멈추기
            if (Input.GetButtonUp("Horizontal"))
            {
                rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
            }
        }
    }

    void FixedUpdate()
    {
        if (isMove)
        {
            move();
            notJump();
        }

    }

    // 좌우로 움직임
    void move()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(inputHorizontal * walkSpeed, rigid.velocity.y);

        if (rigid.velocity.x > 0)
        {
            ani.SetBool("walking", true);
            transform.localScale = new Vector3(-playerSize, playerSize, 1);
        }
        else if (rigid.velocity.x < 0)
        {
            ani.SetBool("walking", true);
            transform.localScale = new Vector3(playerSize, playerSize, 1);
        }
        else 
        {
            ani.SetBool("walking", false);
        }
    }
    // 점프
    void jump() 
    {
        if (Input.GetButtonDown("Jump") && isJump == false && isSit == false)
        {
            ani.SetBool("jumping", true);
            isJump = true;
            // 점프를 할 때 속도를 초기화해서 좀 더 게임적으로
            rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }

    // 눕기
    void sit()
    {
        // 앉은 상태
        if (!findLadder && Input.GetButtonDown("Sit"))
        {
            // 애니매이션 조절
            ani.SetBool("sitting", true);
            // 콜라이더 조절
            standCol.enabled = false;
            boxCol.enabled = true;
            // 스피드 조절
            walkSpeed = sitSpeed;

            isSit = true;
        }

        // 다시 일어섬
        if (!findLadder && Input.GetButtonUp("Sit"))
        {
            ani.SetBool("sitting", false);
            standCol.enabled = true;
            boxCol.enabled = false;
            walkSpeed = maxSpeed;

            isSit = false;
        }
    }

    // 사다리 타기
    void ladder()
    {
        // 사다리 타기 판정
        RaycastHit2D hitInfo = Physics2D.Raycast(ground.transform.position, Vector2.up, rcDistance, whatIsLadder);
        Debug.DrawRay(ground.transform.position, Vector2.up * rcDistance, new Color(0, 1, 0));
        if (hitInfo.collider != null)
        {
            findLadder = true;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isLadder = true;
            }
        }
        else
        {
            isLadder = false;
            findLadder = false;
        }

        if (isLadder == true)
        {
            ani.SetBool("ladder", true);
            inputVertical = Input.GetAxisRaw("Vertical");

            if (inputVertical == 0) ani.SetFloat("climbingSpeed", 0.0f);
            else ani.SetFloat("climbingSpeed", 0.7f);

            rigid.velocity = new Vector2(rigid.position.x, inputVertical * walkSpeed);
            rigid.gravityScale = 0;
        }
        else
        {
            ani.SetBool("ladder", false);
            rigid.gravityScale = 2.0f;
        }
    }

    void notJump() 
    {
        // 더블 점프 방지
        if (rigid.velocity.y < 0)
        {
            // 레이케스트 그리기
            Debug.DrawRay(rigid.position, Vector2.down * 2, new Color(1, 0, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("tiles"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 3.0f)
                {
                    ani.SetBool("jumping", false);
                    isJump = false; 
                    // Debug.Log(rayHit.collider.name);
                }
            }
        }
    }
}
