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
    public int nextMove; // �ൿ��ǥ�� ������ ����
    public bool isMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 1.5f);     // 1.5�ʸ��� Think �Լ� ����
        isMove = true;
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

        anim.SetInteger("isWalking", nextMove);             // 0�̸� ���� �ִ�, �ƴϸ� ������ �ִ�
        
        if (nextMove != 0)
        {
            spriteRenderer.flipX = (nextMove == -1);        // ó���� �������� ���� ����, �������� �̵� �� ��ȯ
        }

        float nextThinkTime = Random.Range(1f, 2f);         // ������ ������ �ٽ� �����ϱ�

        Invoke("Think", nextThinkTime);
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


    IEnumerator TurnDelay()
    {
        nextMove = nextMove * (-1);
        spriteRenderer.flipX = (nextMove == -1);     // nextMove�� -1�̸� ���� �ٲٱ�
        CancelInvoke();
        Invoke("Think", 2.0f);
        yield return new WaitForSeconds(0.5f);
    }

    public void stun() {
        CancelInvoke();

        nextMove = 0;
        anim.SetInteger("isWalking", nextMove);

        Invoke("Think", 3.0f);
    }
}

