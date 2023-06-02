using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StatContent : MonoBehaviour
{
    [Header("각 스탯 내용을 넣을 텍스트 오브젝트 삽입")]
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI endurnaceText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI intelligenceText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI maxMoneyText;


    // PlayerStat의 데이터를 저장한다.
    int money;               // 돈
    int maxMoney;            // 목표한 돈
    int exp;                 // 경험치
    int endurance;           // 지구력
    int strength;            // 근력
    int intelligence;        // 지능

    int remind;            // 목표치를 퍼센트로 나타낸다.

    private void Update()
    {
        statUpdate();
    }

    void statUpdate()
    {
        // 저장된 데이터를 가져온다.
        exp = PlayerStat.instance.exp;
        endurance = PlayerStat.instance.endurance;
        strength = PlayerStat.instance.strength;
        intelligence = PlayerStat.instance.intelligence;
        money = PlayerStat.instance.money;
        maxMoney = PlayerStat.instance.maxMoney;

        remind = maxMoney - money;

        expText.text = $"경험치 : {exp}";
        endurnaceText.text = $"인내심 : {endurance}";
        strengthText.text = $"근 력 : {strength}";
        intelligenceText.text = $"지 능 : {intelligence}";
        moneyText.text = $"현재 : {money}원";
        maxMoneyText.text = $"목표 : {maxMoney}원\n\n" +
            $"앞으로 {remind}원 \n남았습니다. ";
    }
}
