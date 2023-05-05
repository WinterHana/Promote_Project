using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rigidbody , playerMove ���� 
 * 1. ��������(Linear Drag) : 1
 * 2. �߷� ����(Gravity Scale) : 2
 * 3. playerSize = 0.8
 * 4. MaxSpeed = 6
 * 5. JumpPower = 13
 * 6. deceleration = 3.0
 * 7. climbSpeed = 3
 */
public class PlayerMove : MonoBehaviour
{
    const float GRAVITYSCALE = 2.0f;
    [Header("�÷��̾� ���� ���� ����")]
    [SerializeField] float playerSize;
    [SerializeField] float maxSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float jumpPower;
    [SerializeField] float sitSpeed;
    [SerializeField] float climbSpeed;
    [SerializeField] float rcDistance;
    [SerializeField] LayerMask whatIsLadder;
    [Space]
    [Header("�÷��̾� ������ ����")]
    public static bool isMove;      // ���� �� �� ���Ƽ� static���� ����

    Rigidbody2D rigid;
    Animator ani;
    CapsuleCollider2D standCol;
    BoxCollider2D boxCol;
    GameObject ground;
    
    bool isJump;            // ���� �������� Ȯ��
    bool isSit;             // ���� �������� Ȯ��
    bool isLadder;          // ��ٸ� �������� Ȯ��
    bool findLadder;        // �����ɽ�Ʈ�� ��ٸ��� ã����
    float inputHorizontal;
    float inputVertical;

    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        standCol = GetComponent<CapsuleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();
        ground = transform.GetChild(0).gameObject;
        // ������Ʈ�� ������ ���� ����
        rigid.freezeRotation = true;

        // ���� ��ǵ��� ����
        isJump = false;
        isSit = false;
        isLadder = false;
        findLadder = false;

        // �ݶ��̴��� 2���ε�, �� ���� ���� ĸ��, ���� ���� �ڽ��� �Ѵ�.
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
            // Ű���忡�� ���� ���� �� ���� ���߱�
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

    // �¿�� ������
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
    // ����
    void jump() 
    {
        if (Input.GetButtonDown("Jump") && isJump == false && isSit == false)
        {
            ani.SetBool("jumping", true);
            isJump = true;
            // ������ �� �� �ӵ��� �ʱ�ȭ�ؼ� �� �� ����������
            rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
        }
    }

    // ����
    void sit()
    {
        // ���� ����
        if (!findLadder && Input.GetButtonDown("Sit"))
        {
            // �ִϸ��̼� ����
            ani.SetBool("sitting", true);
            // �ݶ��̴� ����
            standCol.enabled = false;
            boxCol.enabled = true;
            // ���ǵ� ����
            walkSpeed = sitSpeed;

            isSit = true;
        }

        // �ٽ� �Ͼ
        if (!findLadder && Input.GetButtonUp("Sit"))
        {
            ani.SetBool("sitting", false);
            standCol.enabled = true;
            boxCol.enabled = false;
            walkSpeed = maxSpeed;

            isSit = false;
        }
    }

    // ��ٸ� Ÿ��
    void ladder()
    {
        // ��ٸ� Ÿ�� ����
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
        // ���� ���� ����
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
