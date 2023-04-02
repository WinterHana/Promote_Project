using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackObject : MonoBehaviour
{
    [SerializeField] float damege;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInfo info = collision.GetComponent<PlayerInfo>();
            info.health.MyCurrentValue -= damege;
        }
    }
}
