using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventController : MonoBehaviour
{
    [SerializeField] PlayerHPController playerHP;
    private void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
    }

    public void UpEndurance()
    {    
        if (PlayerStat.instance.working >= 10)
        {
            // �ൿġ ����
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI����
            PlayerStat.instance.working -= 10;           // ���� ���ȿ���
            // �γ��� ����
            PlayerStat.instance.endurance++;
            // ȿ�� : �ִ� ü�� ����.
            PlayerStat.instance.maxHealth = 100 + PlayerStat.instance.endurance * 10;
            PlayerStat.instance.health += 10;
            // ȿ�� -> UI�� ���� �ݿ��ϱ�
            playerHP.Hphealth.MyMaxValue = PlayerStat.instance.maxHealth;
            playerHP.Hphealth.MyCurrentValue = PlayerStat.instance.health;

        }
    }

    public void UpStrength()
    {
        if (PlayerStat.instance.working >= 10)
        {
            // �ൿġ ����
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI����
            PlayerStat.instance.working -= 10;           // ���� ���ȿ���
            // �� ����
            PlayerStat.instance.strength++;
            // ȿ�� : ���� ������ ����
            PlayerStat.instance.atkDamege = 10 + PlayerStat.instance.strength * 5;
        }
    }

    public void UpIntelligence()
    {
        if (PlayerStat.instance.working >= 10)
        {
            // �ൿġ ����
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI����
            PlayerStat.instance.working -= 10;           // ���� ���ȿ���
            // ���� ����
            PlayerStat.instance.intelligence++;
            
        }
    }
}
