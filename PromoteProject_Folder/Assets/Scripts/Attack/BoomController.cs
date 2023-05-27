using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    EnemyStat stat;

    private void Start()
    {
        Invoke("DestroyBoom", 20f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            stat = collision.GetComponent<EnemyStat>();
            EnemyMove enemyMove = collision.gameObject.GetComponent<EnemyMove>();

            stat.attacked(PlayerStat.instance.atkDamege);

            enemyMove.stun();

            Destroy(gameObject);
        }
    }

    void DestroyBoom() {
        Destroy(gameObject);
    }
}
