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
            // 효과 : 최대 체력 증가.
            PlayerStat.instance.maxHealth = 100 + PlayerStat.instance.endurance * 10;
            PlayerStat.instance.health += 10;
            // 효과 -> UI에 직접 반영하기
            playerHP.Hphealth.MyMaxValue = PlayerStat.instance.maxHealth;
            playerHP.Hphealth.MyCurrentValue = PlayerStat.instance.health;

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
            // 효과 : 공격 데미지 증가
            PlayerStat.instance.atkDamege = 10 + PlayerStat.instance.strength * 5;
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
