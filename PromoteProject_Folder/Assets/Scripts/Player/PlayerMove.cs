using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rigidbody , playerMove ���� 
 * 1. ��������(Linear Drag) : 1
 * 2. ����(Mass) : 50
 * 2. �߷� ����(Gravity Scale) : 1
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

    [Header("���� ���� ���� ����")]
    public GameObject prfBoom;
    public float atkSpeed;              // ���� ����
    public bool isAttacked;             // ������ ����
    float curTime;                      // ���� �ð� ����
    float atkDmg;

    [Header("ȿ���� ���� ���� ����")]
    public AudioSource runSound;
    public AudioSource jumpSound;
    public AudioSource AttackSound;
    public AudioSource AttackedSound;

    Rigidbody2D rigid;
    Animator ani;
    CapsuleCollider2D standCol;
    BoxCollider2D boxCol;
    GameObject ground;
    PlayerLadderFinder LadderFinder;
    SpriteRenderer spriteRenderer;
    PlayerHPController playerHP;
    AttackCoolTimeController atkCooltime;

    bool isJump;            // ���� �������� Ȯ��
    bool isSit;             // ���� �������� Ȯ��
    bool isLadder;          // ��ٸ� �������� Ȯ��
    bool findLadder;        // �����ɽ�Ʈ�� ��ٸ��� ã����
    bool isDamage;          // ���� ���ߴ����� ���� ���θ� �˷��ش�.
    bool isRun;
    float inputHorizontal;
    float inputVertical;
    
    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        standCol = GetComponent<CapsuleCollider2D>();
        boxCol = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
        atkCooltime = GameObject.FindGameObjectWithTag("AttackCoolTime").GetComponent<AttackCoolTimeController>();

        ground = transform.GetChild(0).gameObject;
        LadderFinder = transform.GetChild(1).gameObject.GetComponent<PlayerLadderFinder>();
        // ������Ʈ�� ������ ���� ����
        rigid.freezeRotation = true;

        // ���� ��ǵ��� ����
        isJump = false;
        isSit = false;
        isLadder = false;
        isDamage = false;
        findLadder = false;

        // �ݶ��̴��� 2���ε�, �� ���� ���� ĸ��, ���� ���� �ڽ��� �Ѵ�.
        standCol.enabled = true;
        boxCol.enabled = false;

        walkSpeed = maxSpeed;

        isMove = true;

        // ���� �ð� �ʱ�ȭ
        curTime = atkSpeed;
        atkCooltime.time_cooltime = atkSpeed;
    }

    private void Start()
    {
        // ���� ����� ����
        atkDmg = PlayerStat.instance.atkDamege;
    }
    void Update()
    {
        if (isMove)
        {
            jump();
            sit();
            ladder();
            attack();
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
        else
        {
            // �� ������ ���� ���ִ� �ڼ� ����
            rigid.velocity = new Vector2(0, 0);
            ani.SetBool("walking", false);
            ani.SetBool("jumping", false);
            ani.SetBool("sitting", false);
            isJump = false;
            isSit = false;
        }
        die();
    }

    // �¿�� ������
    void move()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(inputHorizontal * walkSpeed, rigid.velocity.y);
        if (rigid.velocity.x > 0)
        {
            isRun = true;
            ani.SetBool("walking", true); 
            transform.localScale = new Vector3(-playerSize, playerSize, 1);
        }
        else if (rigid.velocity.x < 0)
        {
            isRun = true;
            ani.SetBool("walking", true);
            transform.localScale = new Vector3(playerSize, playerSize, 1);
        }
        else 
        {
            isRun = false;
            ani.SetBool("walking", false);
        }

        if (isRun && !isJump) {
            if(!runSound.isPlaying )runSound.Play();
        } 
        else runSound.Stop();
    }
    // ����
    void jump() 
    {
        if (Input.GetButtonDown("Jump") && isJump == false && isSit == false)
        {
            ani.SetBool("jumping", true);
            jumpSound.Play();
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

        // ��ٸ��� ���� �����ϱ�
        // if (hitInfo.collider != null)
        if(LadderFinder.findLadder)
        {
            findLadder = true;
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
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
            inputHorizontal = Input.GetAxisRaw("Horizontal");
            Vector2 pos = new Vector2(rigid.position.x + 0.7f * inputHorizontal, rigid.position.y);
            RaycastHit2D rayHit = Physics2D.Raycast(pos, Vector3.down, 2.0f, LayerMask.GetMask("tiles", "enemy"));
            Debug.DrawRay(pos, Vector2.down * 2.0f, new Color(1, 0, 0));

            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                {
                    ani.SetBool("jumping", false);
                    isJump = false; 
                    // Debug.Log(rayHit.collider.name);
                }
            }
        }
    }

    void attack()
    {
        if (curTime <= 0)
        {
            isAttacked = true;
            if (!isJump && !isLadder && !isJump && Input.GetButtonDown("Attack"))
            {
                GameObject boom = Instantiate(prfBoom) as GameObject;
                boom.transform.position = new Vector2(ground.transform.position.x, ground.transform.position.y + 0.4f);
                AttackSound.Play();
                atkCooltime.Trigger_Skill();
                isAttacked = false;
                curTime = atkSpeed;
            }
        }
        else
        {
            curTime -= Time.deltaTime;
        }
    }

    // �ǰ� ������ ��
    public void OnDamaged(float damage, Transform tr)
    {
        Vector2 attackedVelocity = Vector2.zero;

        if (!isDamage) {
            AttackedSound.Play();
            playerHP.Hphealth.MyCurrentValue -= damage;
            PlayerStat.instance.health -= damage;
            isDamage = true;

            if (tr.position.x > transform.position.x)
                attackedVelocity = new Vector2(-5f, 5f);
            else
                attackedVelocity = new Vector2(5f, 5f);

            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            ani.SetTrigger("attacked");

            isMove = false;
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);

            Invoke("OffDamaged", 1f);       // ���� �ð�
            Invoke("canMove", 0.5f);        // ���� �ð�
        }
    }

    // �ǰ� �� ���󺹱� ���� �Լ�
    void canMove()
    {
        isMove = true;
    }

    void OffDamaged()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
        isDamage = false;
    }

    void die()
    {
        if (PlayerStat.instance.health <= 0)
        {
            ani.SetBool("die", true);
            isMove = false;
        }
    }
}
