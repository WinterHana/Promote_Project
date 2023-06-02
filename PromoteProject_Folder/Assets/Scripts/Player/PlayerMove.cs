using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * rigidbody , playerMove 설정 
 * 1. 공기저항(Linear Drag) : 1
 * 2. 무게(Mass) : 50
 * 2. 중력 비중(Gravity Scale) : 1
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

    [Header("공격 관련 스탯 정리")]
    public GameObject prfBoom;
    public float atkSpeed;              // 공격 간격
    public bool isAttacked;             // 공격한 여부
    float curTime;                      // 간격 시간 저장
    float atkDmg;

    [Header("효과음 관련 에셋 정리")]
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

    bool isJump;            // 점프 상태인지 확인
    bool isSit;             // 앉은 상태인지 확인
    bool isLadder;          // 사다리 상태인지 확인
    bool findLadder;        // 레이케스트가 사다리를 찾았음
    bool isDamage;          // 공격 당했는지에 대한 여부를 알려준다.
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
        // 오브젝트가 구르는 현상 방지
        rigid.freezeRotation = true;

        // 각종 모션들의 상태
        isJump = false;
        isSit = false;
        isLadder = false;
        isDamage = false;
        findLadder = false;

        // 콜라이더가 2개인데, 서 있을 때는 캡슐, 앉을 때는 박스로 한다.
        standCol.enabled = true;
        boxCol.enabled = false;

        walkSpeed = maxSpeed;

        isMove = true;

        // 공격 시간 초기화
        curTime = atkSpeed;
        atkCooltime.time_cooltime = atkSpeed;
    }

    private void Start()
    {
        // 공격 대미지 설정
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
        else
        {
            // 안 움직일 때는 서있는 자세 유지
            rigid.velocity = new Vector2(0, 0);
            ani.SetBool("walking", false);
            ani.SetBool("jumping", false);
            ani.SetBool("sitting", false);
            isJump = false;
            isSit = false;
        }
        die();
    }

    // 좌우로 움직임
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
    // 점프
    void jump() 
    {
        if (Input.GetButtonDown("Jump") && isJump == false && isSit == false)
        {
            ani.SetBool("jumping", true);
            jumpSound.Play();
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

        // 사다리의 조건 수정하기
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
        // 더블 점프 방지
        if (rigid.velocity.y < 0)
        {
            // 레이케스트 그리기
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

    // 피격 당했을 때
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

            Invoke("OffDamaged", 1f);       // 무적 시간
            Invoke("canMove", 0.5f);        // 경직 시간
        }
    }

    // 피격 후 원상복귀 관련 함수
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
