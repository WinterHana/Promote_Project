using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    CircleCollider2D circle;
    Animator anim;
    protected int nextMove; // �ൿ��ǥ�� ������ ����

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        rigid.freezeRotation = true;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 1.5f);     // 1.5�ʸ��� Think �Լ� ����
    }

    private void Start()
    {
        currentSpeed = wanderSpeed;
    }

    void FixedUpdate()
    {
        move();
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);                     // -1�̸� ����, 0�̸� ���߱�, 1�̸� ���������� �̵�
        Debug.Log(nextMove);
        anim.SetInteger("isWalking", nextMove);             // 0�̸� ���� �ִ�, �ƴϸ� ������ �ִ�
        
        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == -1);        // ó���� �������� ���� ����, �������� �̵� �� ��ȯ
        }

        float nextThinkTime = Random.Range(1f, 2f);

        Invoke("Think", nextThinkTime);                     // 5�ʸ��� Think �Լ� ���� by ����Լ�
    }

    void move()
    {
        // �� �������� ������
        rigid.velocity = new Vector2(nextMove * currentSpeed, rigid.velocity.y);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer)
        {
            currentSpeed = pursuitSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentSpeed = wanderSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (circle != null)
        {
            Gizmos.DrawWireSphere(transform.position, circle.radius);
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
}
