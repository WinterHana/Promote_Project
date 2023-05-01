using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// 직렬화
public class PlayerStat : MonoBehaviour
{
    public static PlayerStat instance;

    public int exp;                 // 경험치
    public int endurance;           // 지구력
    public int strength;            // 근력
    public int intelligence;        // 지능
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
}
