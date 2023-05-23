using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackObject : MonoBehaviour
{
    [SerializeField] float damage;
    protected bool isAttack;

    protected virtual void Start()
    {
        isAttack = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isAttack) {
                PlayerMove player = collision.GetComponent<PlayerMove>();
                player.OnDamaged(damage, gameObject.transform);
            }
        }
    }
}
