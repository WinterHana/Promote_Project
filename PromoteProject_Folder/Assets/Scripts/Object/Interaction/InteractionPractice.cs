using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPractice : InteractionObject
{
    [SerializeField] PlayerHPController playerHP;
    delegate void Action();
    [SerializeField] int statNo;
    // [SerializeField] SelectPopUpManager selectPopUpManager;
    private void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
        // selectPopUpManager = GameObject.FindGameObjectWithTag("SelectPopUp").GetComponent<SelectPopUpManager>();

        // �ɷ�ġ ���� �Լ� �ֱ�
        Action[] action = { EnduranceControl, StrengthControl, IntelligenceControl };
    }

    public void UpEndurance()
    {
        SelectPopUpManager.instance.OpenPopUp(1001);
        StartCoroutine(SelectCoroutine(EnduranceControl));
    }

    public void UpStrength()
    {
        SelectPopUpManager.instance.OpenPopUp(1002);
        StartCoroutine(SelectCoroutine(StrengthControl));
    }

    public void UpIntelligence()
    {
        SelectPopUpManager.instance.OpenPopUp(1003);
        StartCoroutine(SelectCoroutine(IntelligenceControl));
    }

    void EnduranceControl()
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
        };
    }

    void StrengthControl()
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

    void IntelligenceControl()
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

    IEnumerator SelectCoroutine(Action action)
    {
        yield return new WaitUntil(() => !SelectPopUpManager.instance.isSelect);

        if (SelectPopUpManager.instance.sign)
        {
            action();
        }
    }


    public override void Interaction()
    {
        if (statNo == 1) UpEndurance();
        if (statNo == 2) UpStrength();
        if (statNo == 3) UpIntelligence();
    }
}
