using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rigidbody 설정 
 * 1. 공기저항(Linear Drag) : 1
 * 2. 중력 비중(Gravity Scale) : 2
 * 3. MaxSpeed = 3
 * 4. JumpPower = 7
 */
public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();

        // 최대 속도, 점프 조정
        // maxSpeed = 3.0f;
        // jumpPower = 3.0f;

        // 오브젝트가 구르는 현상 방지
        rigid.freezeRotation = true;
    }
    
    void Update()
    {
        jump();
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
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        }
    }
    // 점프
    void jump() 
    {
        if (Input.GetButtonDown("Jump"))
        {
            // 점프를 할 때 속도를 초기화해서 좀 더 게임적으로
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    void raycast() 
    {
        if (rigid.velocity.y < 0)
        {
            // 레이케스트 그리기
            Debug.DrawRay(rigid.position, Vector2.down, new Color(1, 0, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("tiles"));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.0f)
                {
                    Debug.Log(rayHit.collider.name);
                }
            }
        }
    }
}
