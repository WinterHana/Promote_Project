using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// �ǽð����� ������ �����ϰ� �Ѵ�.
public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int exp;                 // ����ġ
    public int money;               // ���� ��
    public int maxMoney;            // ��ǥ�� ��
    public int endurance;           // ������
    public int strength;            // �ٷ�
    public int intelligence;        // ����
    public int atkDamege;           // ������ �ִ� ������
    public float health;            // ���� ü��
    public float maxHealth;         // �ִ� ü��
    public float working;           // ���� �Ƿε�
    public float maxWorking;        // �ִ� �Ƿε�
    public int times;               // ���� �ð�
    public int dialogue;            // ��ȭ ����

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            saveData(0, 0, 50000000, 0, 0, 0, 10, 200, 200, 50, 50, 0, 0);
            DontDestroyOnLoad(this);
        }
        else { 
            Destroy(gameObject);
        }
    }

    public void saveData(int _exp, int _money, int _maxMoney, int _endurance, int _strength,
    int _intelligence, int _atkDamege, float _health, float _maxHealth, 
    float _working, float _maxWorking, int _times, int _dialogue)
    {
        exp = _exp;
        money = _money;
        maxMoney = _maxMoney;
        endurance = _endurance;
        strength = _strength;
        intelligence = _intelligence;
        atkDamege = _atkDamege;
        health = _health;
        maxHealth = _maxHealth;
        working = _working;
        maxWorking = _maxWorking;
        times = _times;
        dialogue = _dialogue;
    }
}
