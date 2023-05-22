using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    EnemyStat stat;
    public float pushForce = 1000000f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            stat = collision.GetComponent<EnemyStat>();
            EnemyMove enemyMove = collision.gameObject.GetComponent<EnemyMove>();

            stat.attacked(40);

            enemyMove.stun();

            Destroy(gameObject);
        }
    }
}
