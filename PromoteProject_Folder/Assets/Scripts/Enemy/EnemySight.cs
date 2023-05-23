using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 시야를 통해 움직임 속도 제어
public class EnemySight : MonoBehaviour
{
    CircleCollider2D circle;
    EnemyMove enemyMove;

    private void OnDrawGizmos()
    {
        if (circle != null)
        {
            Gizmos.DrawWireSphere(transform.position, circle.radius);
        }
    }

    void Awake()
    {
        circle = GetComponent<CircleCollider2D>();
        enemyMove = transform.parent.GetComponent<EnemyMove>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && enemyMove.followPlayer)
        {
            enemyMove.currentSpeed = enemyMove.pursuitSpeed;
            enemyMove.isPursuit = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemyMove.currentSpeed = enemyMove.wanderSpeed;
            enemyMove.isPursuit = false;
        }
    }
}
