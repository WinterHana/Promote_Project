using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackObject : MonoBehaviour
{
    [SerializeField] float damege;
    [SerializeField] PlayerHPController playerHP;

    protected virtual void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHP.Hphealth.MyCurrentValue -= damege;
            PlayerStat.instance.health -= damege;
        }
    }
}
