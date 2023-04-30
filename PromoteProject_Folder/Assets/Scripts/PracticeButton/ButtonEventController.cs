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
