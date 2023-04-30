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

    // �Ľ��� �����͸� �����Ѵ�.
    string exp;               // ����ġ
    string endurance;         // ������
    string strength;          // �ٷ�
    string intelligence;      // ����
    string health;          // ü��
    string maxHealth;       // �ִ� ü��
    string working;         // �Ƿε�
    string maxWorking;      // �ִ� �Ƿε�

    private void Awake()
    {

    }

    private void Update()
    {
        statUpdate();
    }

    void statUpdate()
    {
        // ����� �����͸� �����´�.
        exp = PlayerStat.instance.exp.ToString();
        endurance = PlayerStat.instance.endurance.ToString();
        strength = PlayerStat.instance.strength.ToString();
        intelligence = PlayerStat.instance.intelligence.ToString();
        health = PlayerStat.instance.health.ToString();
        maxHealth = PlayerStat.instance.maxHealth.ToString();
        working = PlayerStat.instance.working.ToString();
        maxWorking = PlayerStat.instance.maxWorking.ToString();

        // �ؽ�Ʈ�� ������ Text ������Ʈ�� �����´�
        statText.text =
            "����ġ : " + exp + "\n" +
            "�γ��� : " + endurance + "\n" +
            "�� : " + strength + "\n" +
            "���� : " + intelligence + "\n" +
            "���� ü�� : " + health + "\n" +
            "�ִ� ü�� : " + maxHealth + "\n" +
            "���� �Ƿε� : " + working + "\n" +
            "�ִ� �Ƿε� : " + maxWorking + "\n";
    }
}
