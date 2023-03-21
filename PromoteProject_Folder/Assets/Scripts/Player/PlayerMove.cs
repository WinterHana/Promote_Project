using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rigidbody ���� 
 * 1. ��������(Linear Drag) : 1
 * 2. �߷� ����(Gravity Scale) : 2
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
    bool isJump;            // ���� �������� Ȯ��
    bool isSit;           // ���� �������� Ȯ��
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        standCol = GetComponent<CapsuleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();

        // ������Ʈ�� ������ ���� ����
        rigid.freezeRotation = true;
        isJump = false;
        isSit = false;

        // �ݶ��̴��� 2���ε�, �� ���� ���� ĸ��, ���� ���� �ڽ��� �Ѵ�.
        standCol.enabled = true;
        boxCol.enabled = false;
    }
    
    void Update()
    {
        jump(); 
        sit();
        // Ű���忡�� ���� ���� �� ���� ���߱�
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

    // �¿�� ������
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
    // ����
    void jump() 
    {
        if (Input.GetButtonDown("Jump") && isJump == false && isSit == false)
        {
            ani.SetBool("jumping", true);
            isJump = true;
            // ������ �� �� �ӵ��� �ʱ�ȭ�ؼ� �� �� ����������
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }

    // ����
    void sit()
    {
        // ���� ����
        if (Input.GetButtonDown("Sit"))
        {
            // �ִϸ��̼� ����
            ani.SetBool("sitting", true);
            // �ݶ��̴� ����
            standCol.enabled = false;
            boxCol.enabled = true;
            // ���ǵ� ����
            maxSpeed /= deceleration;

            isSit = true;
        }

        // �ٽ� �Ͼ
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
            // �����ɽ�Ʈ �׸���
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
