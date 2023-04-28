using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackObject : MonoBehaviour
{
    [SerializeField] float damege;
    [SerializeField] PlayerInfo playerInfo;

    protected virtual void Start()
    {
        playerInfo = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerInfo>();
        if (playerInfo == null) Debug.Log("PlayerAttackObject ��ã��");
        else Debug.Log("PlayerAttackObject ã��");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInfo.Hphealth.MyCurrentValue -= damege;
            PlayerStat.instance.health -= damege;
        }
    }
}
