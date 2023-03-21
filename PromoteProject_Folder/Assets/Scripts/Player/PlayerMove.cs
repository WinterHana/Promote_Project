using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rigidbody 설정 
 * 1. 공기저항(Linear Drag) : 1
 * 2. 중력 비중(Gravity Scale) : 2
 * 3. playerSize = 0.8
 * 4. MaxSpeed = 6
 * 5. JumpPower = 13
 * 6. deceleration = 3.0
 */
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float playerSize;
    [SerializeField] float maxSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float deceleration;
    Rigidbody2D rigid;
    Animator ani;
    CapsuleCollider2D standCol;
    BoxCollider2D boxCol;
    bool isJump;            // 점프 상태인지 확인
    bool isSit;           // 앉은 상태인지 확인
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        standCol = GetComponent<CapsuleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();

        // 오브젝트가 구르는 현상 방지
        rigid.freezeRotation = true;
        isJump = false;
        isSit = false;

        // 콜라이더가 2개인데, 서 있을 때는 캡슐, 앉을 때는 박스로 한다.
        standCol.enabled = true;
        boxCol.enabled = false;
    }
    
    void Update()
    {
        jump(); 
        sit();
        // 키보드에서 손을 땠을 때 완전 멈추기
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }

    void FixedUpdate()
    {
        move();
        raycast();
    }

    // 좌우로 움직임
    void move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            ani.SetBool("walking", true);
            transform.localScale = new Vector3(-playerSize, playerSize, 1);
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            ani.SetBool("walking", true);
            transform.localScale = new Vector3(playerSize, playerSize, 1);
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
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
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    // 눕기
    void sit()
    {
        // 앉은 상태
        if (Input.GetButtonDown("Sit"))
        {
            // 애니매이션 조절
            ani.SetBool("sitting", true);
            // 콜라이더 조절
            standCol.enabled = false;
            boxCol.enabled = true;
            // 스피드 조절
            maxSpeed /= deceleration;

            isSit = true;
        }

        // 다시 일어섬
        if (Input.GetButtonUp("Sit"))
        {
            ani.SetBool("sitting", false);
            standCol.enabled = true;
            boxCol.enabled = false;
            maxSpeed *= deceleration;

            isSit = false;
        }
    }
    void raycast() 
    {
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
