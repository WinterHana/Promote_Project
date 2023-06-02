using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StatContent : MonoBehaviour
{
    [Header("�� ���� ������ ���� �ؽ�Ʈ ������Ʈ ����")]
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private TextMeshProUGUI endurnaceText;
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI intelligenceText;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI maxMoneyText;


    // PlayerStat�� �����͸� �����Ѵ�.
    int money;               // ��
    int maxMoney;            // ��ǥ�� ��
    int exp;                 // ����ġ
    int endurance;           // ������
    int strength;            // �ٷ�
    int intelligence;        // ����

    int remind;            // ��ǥġ�� �ۼ�Ʈ�� ��Ÿ����.

    private void Update()
    {
        statUpdate();
    }

    void statUpdate()
    {
        // ����� �����͸� �����´�.
        exp = PlayerStat.instance.exp;
        endurance = PlayerStat.instance.endurance;
        strength = PlayerStat.instance.strength;
        intelligence = PlayerStat.instance.intelligence;
        money = PlayerStat.instance.money;
        maxMoney = PlayerStat.instance.maxMoney;

        remind = maxMoney - money;

        expText.text = $"����ġ : {exp}";
        endurnaceText.text = $"�γ��� : {endurance}";
        strengthText.text = $"�� �� : {strength}";
        intelligenceText.text = $"�� �� : {intelligence}";
        moneyText.text = $"���� : {money}��";
        maxMoneyText.text = $"��ǥ : {maxMoney}��\n\n" +
            $"������ {remind}�� \n���ҽ��ϴ�. ";
    }
}
