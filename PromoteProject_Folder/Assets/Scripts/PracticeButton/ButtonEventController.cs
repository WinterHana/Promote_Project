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
            // 행동치 감소
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI에서
            PlayerStat.instance.working -= 10;           // 실제 스탯에서
            // 인내심 증가
            PlayerStat.instance.endurance++;
        }
    }

    public void UpStrength()
    {
        if (PlayerStat.instance.working >= 10)
        {
            // 행동치 감소
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI에서
            PlayerStat.instance.working -= 10;           // 실제 스탯에서
            // 힘 증가
            PlayerStat.instance.strength++;
        }
    }

    public void UpIntelligence()
    {
        if (PlayerStat.instance.working >= 10)
        {
            // 행동치 감소
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI에서
            PlayerStat.instance.working -= 10;           // 실제 스탯에서
            // 지능 증가
            PlayerStat.instance.intelligence++;
        }
    }
}
