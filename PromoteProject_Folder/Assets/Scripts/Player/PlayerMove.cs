using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    Rigidbody2D rigid;
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();

        // �ִ� �ӵ�, ���� ����
        // maxSpeed = 3.0f;
        // jumpPower = 3.0f;

        // ������Ʈ�� ������ ���� ����
        rigid.freezeRotation = true;
    }
    void Start()
    {
        
    }
    
    void Update()
    {
        jump();
        // Ű���忡�� ���� ���� �� ���� ���߱�
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }
    }


    void FixedUpdate()
    {
        move();

        // �����ɽ�Ʈ �׸���
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

    // �¿�� ������
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
    // ����
    void jump() 
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
}
