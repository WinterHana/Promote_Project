using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 컴포넌트가 없으면 자동으로 추가
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]
public class EnemyWander : MonoBehaviour
{
    public float pursuitSpeed;      // 적이 플레이어를 추적하는 속도
    public float wanderSpeed;       // 평상시의 적 속도
    public float currentSpeed;      // 앞의 둘 중에서 선택할 현재 속도

    public float directionChangeInterval;   // 배화할 방향의 전환 빈도
    public bool followPlayer;               // 추적할 여부 결정

    Coroutine moveCoroutine;        // 현재 실행중인 이동 코루틴 : 적을 목적지로 옮기는 역할
    CircleCollider2D circle;      
    Rigidbody2D rb;
    Animator ani;

    Transform targetTransform = null;       // 적이 플레이어를 추적할 때 플레이어의 위치를 얻음
    Vector3 endPosition;                    // 배회하는 적의 목적지

    private void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        currentSpeed = wanderSpeed;         // 처음에는 배회하는 속도로 적이 이동
        StartCoroutine(WanderRoutine());
    }

    private void Update()
    {
        Debug.DrawLine(rb.position, endPosition, Color.red);
    }

    public IEnumerator WanderRoutine()
    {
        while (true)                                // 적이 계속해서 움직임
        {         
            if (moveCoroutine != null) {            // 적이 이동 중일때 새로운 방향 설정을 위한 코루틴 중지
                StopCoroutine(moveCoroutine);
            }

            moveCoroutine = StartCoroutine(Move(rb, currentSpeed));     // 코루틴 시작 후 참조 저장

            yield return new WaitForSeconds(directionChangeInterval);   // 실행 양보 후 다시 루프
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
                remainingDistnace = (transform.position - endPosition).sqrMagnitude;        // 남은 거리 수정
            }

            yield return new WaitForFixedUpdate();          // 다음 프레임까지 실행 유보
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

    // 시각적으로 배회 알고리즘 확인
    private void OnDrawGizmos()
    {
        if (circle != null) {
            Gizmos.DrawWireSphere(transform.position, circle.radius);
        }
    }
}
