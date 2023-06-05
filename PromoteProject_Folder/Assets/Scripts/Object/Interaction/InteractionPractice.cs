using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPractice : InteractionObject
{
    [SerializeField] PlayerHPController playerHP;
    delegate void Action();
    [SerializeField] int statNo;

    [Header("안내 문구")]
    public ExplainTextController guide;
    private bool isSelecting;

    // [SerializeField] SelectPopUpManager selectPopUpManager;
    private void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
        // selectPopUpManager = GameObject.FindGameObjectWithTag("SelectPopUp").GetComponent<SelectPopUpManager>();

        // 능력치 증가 함수 넣기
        Action[] action = { EnduranceControl, StrengthControl, IntelligenceControl };
    }

    public void UpEndurance()
    {
        PlayerMove.isMove = false;
        SelectPopUpManager.instance.OpenPopUp(1001);
        StartCoroutine(SelectCoroutine(EnduranceControl));
    }

    public void UpStrength()
    {
        PlayerMove.isMove = false;
        SelectPopUpManager.instance.OpenPopUp(1002);
        StartCoroutine(SelectCoroutine(StrengthControl));
    }

    public void UpIntelligence()
    {
        PlayerMove.isMove = false;
        SelectPopUpManager.instance.OpenPopUp(1003);
        StartCoroutine(SelectCoroutine(IntelligenceControl));
    }

    void EnduranceControl()
    {
        if (PlayerStat.instance.working >= 10)
        {
            // 행동치 감소
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI에서
            PlayerStat.instance.working -= 10;           // 실제 스탯에서
            // 인내심 증가
            PlayerStat.instance.endurance++;
            // 효과 : 최대 체력 증가.
            PlayerStat.instance.maxHealth = 200 + PlayerStat.instance.endurance * 10;
            PlayerStat.instance.health += 10;
            // 효과 -> UI에 직접 반영하기
            playerHP.Hphealth.MyMaxValue = PlayerStat.instance.maxHealth;
            playerHP.Hphealth.MyCurrentValue = PlayerStat.instance.health;

            ExplainTextController gud = Instantiate(guide);
            gud.guide = "인내심이 증가하였습니다.";
        }
        else {
            ExplainTextController gud = Instantiate(guide);
            gud.guide = "행동치가 부족합니다.";
        }

    }

    void StrengthControl()
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

            ExplainTextController gud = Instantiate(guide);
            gud.guide = "근력이 증가하였습니다.";
        }
        else
        {
            ExplainTextController gud = Instantiate(guide);
            gud.guide = "행동치가 부족합니다.";
        }
    }

    void IntelligenceControl()
    {
        if (PlayerStat.instance.working >= 10)
        {
            // 행동치 감소
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI에서
            PlayerStat.instance.working -= 10;           // 실제 스탯에서
            // 지능 증가
            PlayerStat.instance.intelligence++;

            ExplainTextController gud = Instantiate(guide);
            gud.guide = "지능이 증가하였습니다.";
        }
        else
        {
            ExplainTextController gud = Instantiate(guide);
            gud.guide = "행동치가 부족합니다.";
        }
    }

    IEnumerator SelectCoroutine(Action action)
    {
        yield return new WaitUntil(() => !SelectPopUpManager.instance.isSelect);

        if (SelectPopUpManager.instance.sign)
        {
            action();
        }
        PlayerMove.isMove = true;
    }


    public override void Interaction()
    {
        if (PlayerMove.isMove == true) {
            if (statNo == 1) UpEndurance();
            if (statNo == 2) UpStrength();
            if (statNo == 3) UpIntelligence();
        }
    }
}
