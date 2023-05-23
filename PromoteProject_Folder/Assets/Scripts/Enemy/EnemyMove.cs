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
    }
    private void Start()
    {
        Invoke("Think", 1f);
        currentSpeed = wanderSpeed;
    }

    void FixedUpdate()
    {
        RaycastHit2D raycast = Physics2D.Raycast(transform.position, transform.right * dir, distance, isLayer);
        if (raycast.collider != null)
        {
            if (Vector2.Distance(transform.position, raycast.collider.transform.position) < atkDistance)
            {
                if (currentTime <= 0)
                {
                    GameObject bulletcopy = Instantiate(bullet);
                    bulletcopy.transform.position = new Vector2(transform.position.x, transform.position.y);
                    Debug.Log(bulletcopy.transform.position);
                    currentTime = cooltime;
                }
            }
        }
        else
        {
            // ������ �� ���� ���� �����δ�.
            if (isMove) move();
        }
        currentTime -= Time.deltaTime;
    }

    void Think()
    {
        isMove = true;                                          // ���� ���̸� �׻� ������ �� �־�� �Ѵ�.
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
            anim.SetBool("isRun", true);
            dir = (target.position.x - transform.position.x < 0) ? -1 : 1;
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
        nextMove = 0;
        anim.SetBool("isRun", false);
        anim.SetInteger("isWalking", nextMove);

        Invoke("Think", 3.0f);
    }


}

