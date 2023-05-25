using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    BoxCollider2D col;               // 공격할 범위 : 직사각형
    PlayerMove player;

    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        player = transform.parent.GetComponent<PlayerMove>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Debug.Log("공격 범위!!");
            if (Input.GetButtonDown("Attack")&& player.isAttacked)
            {
                Debug.Log("공격 성공!");
            }
        }
    }
}
