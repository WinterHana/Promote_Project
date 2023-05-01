using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// ����ȭ
public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int exp;                 // ����ġ
    public int endurance;           // ������
    public int strength;            // �ٷ�
    public int intelligence;        // ����
    public float health;            // ���� ü��
    public float maxHealth;         // �ִ� ü��
    public float working;           // ���� �Ƿε�
    public float maxWorking;        // �ִ� �Ƿε�


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else { 
            Destroy(gameObject);
        }
    }
}
