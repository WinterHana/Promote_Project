using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������Ʈ�� ������ �ڵ����� �߰�
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyWander : MonoBehaviour
{
    public float pursuitSpeed;      // ���� �÷��̾ �����ϴ� �ӵ�
    public float wanderSpeed;       // ������ �� �ӵ�
    public float currentSpeed;      // ���� �� �߿��� ������ ���� �ӵ�

    public float directionChangeInterval;   // ��ȭ�� ������ ��ȯ ��
    public bool followPlayer;               // ������ ���� ����

    Coroutine moveCoroutine;        // ���� �������� �̵� �ڷ�ƾ : ���� �������� �ű�� ����
    CircleCollider2D circle;      
    Rigidbody2D rb;
    Animator ani;

    Transform targetTransform = null;       // ���� �÷��̾ ������ �� �÷��̾��� ��ġ�� ����
    Vector3 endPosition;                    // ��ȸ�ϴ� ���� ������

    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        currentSpeed = wanderSpeed;         // ó������ ��ȸ�ϴ� �ӵ��� ���� �̵�
        StartCoroutine(WanderRoutine());
    }

    private void Update()
    {
        Debug.DrawLine(rb.position, endPosition, Color.red);
    }

    public IEnumerator WanderRoutine()
    {
        while (true)                                // ���� ����ؼ� ������
        {         
            if (moveCoroutine != null) {            // ���� �̵� ���϶� ���ο� ���� ������ ���� �ڷ�ƾ ����
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb, currentSpeed));     // �ڷ�ƾ ���� �� ���� ����

            yield return new WaitForSeconds(directionChangeInterval);   // ���� �纸 �� �ٽ� ����
        }
    }
    public IEnumerator Move(Rigidbody2D rigidBodyToMove, float speed)
    {
        float remainingDistnace = (transform.position - endPosition).sqrMagnitude;

        while (remainingDistnace > float.Epsilon) {
            if (targetTransform != null) { 
                endPosition = targetTransform.position;
            }

            if (rigidBodyToMove != null) {
                ani.SetBool("isWalking", true);
                Vector2 newPosition = Vector2.MoveTowards(rigidBodyToMove.position, endPosition, speed * Time.deltaTime);

                rb.MovePosition(newPosition);
                remainingDistnace = (transform.position - endPosition).sqrMagnitude;        // ���� �Ÿ� ����
            }

            yield return new WaitForFixedUpdate();          // ���� �����ӱ��� ���� ����
        }
        ani.SetBool("isWalking", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && followPlayer) {
            currentSpeed = pursuitSpeed;
            targetTransform = collision.gameObject.transform;

            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb, currentSpeed));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            ani.SetBool("isWalking", false);
            currentSpeed = wanderSpeed;

            if (moveCoroutine != null) {
                StopCoroutine(moveCoroutine);
            }

            targetTransform = null;
        }
    }

    // �ð������� ��ȸ �˰��� Ȯ��
    private void OnDrawGizmos()
    {
        if (circle != null) {
            Gizmos.DrawWireSphere(transform.position, circle.radius);
        }
    }
}
