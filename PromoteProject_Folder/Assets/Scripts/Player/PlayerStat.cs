using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// 실시간으로 스탯을 관리하게 한다.
public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int exp;                 // 경험치
    public int money;               // 현재 돈
    public int maxMoney;            // 목표한 돈
    public int endurance;           // 지구력
    public int strength;            // 근력
    public int intelligence;        // 지능
    public int atkDamege;           // 적에게 주는 데미지
    public float health;            // 현재 체력
    public float maxHealth;         // 최대 체력
    public float working;           // 현재 피로도
    public float maxWorking;        // 최대 피로도


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

    public void saveData(int _exp, int _money, int _maxMoney, int _endurance, int _strength,
    int _intelligence, int _atkDamege, float _health, float _maxHealth, float _working, float _maxWorking)
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
    }
}
