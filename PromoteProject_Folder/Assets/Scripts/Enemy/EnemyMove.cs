using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �⺻���� ������ ����
// ������Ʈ�� ������ �ڵ����� �߰�
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyMove : MonoBehaviour
{
    [Header("Speed ����")]
    public float pursuitSpeed;      // ���� �÷��̾ �����ϴ� �ӵ�
    public float wanderSpeed;       // ������ �� �ӵ�
    public float currentSpeed;      // ���� �� �߿��� ������ ���� �ӵ�

    [Header("���� ���� ����")]
    public bool followPlayer;               // ������ ���� ����

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Transform target;

    [Header("���� ������ ���� ����")]
    public int nextMove;    // �ൿ��ǥ�� ������ ����
    public bool isMove;     // ������ �� �ִ����� ���� ����
    public bool isAttack;   // ������ �� �ִ����� ���� ����
    public bool isPursuit;

    [Header("���� ���� ����")]
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
        // ������ �� ���� ���� �����δ�.
        if (isMove) move();

        // ������ �� ���� ���� �����Ѵ�.
        if (isAttack) attack();
    }

    void Think()
    {
        isMove = true; isAttack = true;                         // ���� ���̸� �׻� ������ �� �־�� �Ѵ�.
        // ��ȸ ����� ���� ���������� �۵��Ѵ�.
        spriteRenderer.color = new Color(1, 1, 1, 1f);
        if (!isPursuit) {
            nextMove = Random.Range(-1, 2);                     // -1�̸� ����, 0�̸� ���߱�, 1�̸� ���������� �̵�
            anim.SetInteger("isWalking", nextMove);             // 0�̸� ���� �ִ�, �ƴϸ� ������ �ִ�
            if (nextMove != 0)
            {
                spriteRenderer.flipX = (nextMove == -1);        // ó���� �������� ���� ����, �������� �̵� �� ��ȯ
            }
        }
        float nextThinkTime = Random.Range(1f, 2f);             // ������ ������ �ٽ� �����ϱ�

        Invoke("Think", nextThinkTime);
    }

    void move()
    {
        // ���� ���
        if (isPursuit) {
            // dir = (target.position.x - transform.position.x < 1) ? -1 : 1;
            // �÷��̾ �Ӹ� ���� �ö� �� ����
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

        // ��ȸ ���
        else {
            anim.SetBool("isRun", false);
            rigid.velocity = new Vector2(nextMove * currentSpeed, rigid.velocity.y);
        }
        
        // �÷���(����) �Ʒ� Ȯ��
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y);
        RaycastHit2D rayHitDown = Physics2D.Raycast(frontVec, Vector2.down, 1.5f, LayerMask.GetMask("tiles"));
        Debug.DrawRay(frontVec, Vector2.down* 1.5f, new Color(0, 1, 0));

        // �÷���(����) �� Ȯ��
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
        spriteRenderer.flipX = (nextMove == -1);     // nextMove�� -1�̸� ���� �ٲٱ�
        CancelInvoke();
        Invoke("Think", 2.0f);
        yield return new WaitForSeconds(0.5f);
    }


    // ������ �Ŵ� �Լ�
    public void stun() {
        // Think ���� ���߱�
        CancelInvoke(); 
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        // ������ ����
        isMove = false;
        isAttack = false;
        nextMove = 0;

        // �ִϸ��̼� Idle�� �ٲٱ�
        anim.SetBool("isRun", false);
        anim.SetBool("isAttack", false);
        anim.SetInteger("isWalking", nextMove);

        Invoke("Think", 3.0f);
    }

    void attack()
    {
        // ���� ���� ���
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * dir, distance, isLayer);

        Debug.DrawRay(transform.position, transform.right * dir * distance, new Color(255, 255, 0));

        if (raycast.collider != null)
        {
            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {
                anim.SetBool("isAttack", true);
                isMove = false;
                Debug.Log("�Ÿ� Ȯ��");
                if (currentTime <= 0)
                {
                    Debug.Log("�� �߻�");
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

