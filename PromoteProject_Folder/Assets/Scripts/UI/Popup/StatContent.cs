using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StatContent : MonoBehaviour
{
    public TextMeshProUGUI statText;

    // 파싱한 데이터를 저장한다.
    string exp;               // 경험치
    string endurance;         // 지구력
    string strength;          // 근력
    string intelligence;      // 지능
    string health;          // 체력
    string maxHealth;       // 최대 체력
    string working;         // 피로도
    string maxWorking;      // 최대 피로도

    private void Awake()
    {

    }

    private void Update()
    {
        statUpdate();
    }

    void statUpdate()
    {
        // 저장된 데이터를 가져온다.
        exp = PlayerStat.instance.exp.ToString();
        endurance = PlayerStat.instance.endurance.ToString();
        strength = PlayerStat.instance.strength.ToString();
        intelligence = PlayerStat.instance.intelligence.ToString();
        health = PlayerStat.instance.health.ToString();
        maxHealth = PlayerStat.instance.maxHealth.ToString();
        working = PlayerStat.instance.working.ToString();
        maxWorking = PlayerStat.instance.maxWorking.ToString();

        // 텍스트를 전시할 Text 오브젝트를 가져온다
        statText.text =
            "경험치 : " + exp + "\n" +
            "인내심 : " + endurance + "\n" +
            "힘 : " + strength + "\n" +
            "지능 : " + intelligence + "\n" +
            "현재 체력 : " + health + "\n" +
            "최대 체력 : " + maxHealth + "\n" +
            "현재 피로도 : " + working + "\n" +
            "최대 피로도 : " + maxWorking + "\n";
    }
}
