using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPractice : InteractionObject
{
    [SerializeField] PlayerHPController playerHP;
    delegate void Action();
    [SerializeField] int statNo;

    [Header("�ȳ� ����")]
    public ExplainTextController guide;
    private bool isSelecting;

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
            // �ൿġ ����
            playerHP.Hpworking.MyCurrentValue -= 10;     // UI����
            PlayerStat.instance.working -= 10;           // ���� ���ȿ���
            // �γ��� ����
            PlayerStat.instance.endurance++;
            // ȿ�� : �ִ� ü�� ����.
            PlayerStat.instance.maxHealth = 200 + PlayerStat.instance.endurance * 10;
            PlayerStat.instance.health += 10;
            // ȿ�� -> UI�� ���� �ݿ��ϱ�
            playerHP.Hphealth.MyMaxValue = PlayerStat.instance.maxHealth;
            playerHP.Hphealth.MyCurrentValue = PlayerStat.instance.health;

            ExplainTextController gud = Instantiate(guide);
            gud.guide = "�γ����� �����Ͽ����ϴ�.";
        }
        else {
            ExplainTextController gud = Instantiate(guide);
            gud.guide = "�ൿġ�� �����մϴ�.";
        }

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

            ExplainTextController gud = Instantiate(guide);
            gud.guide = "�ٷ��� �����Ͽ����ϴ�.";
        }
        else
        {
            ExplainTextController gud = Instantiate(guide);
            gud.guide = "�ൿġ�� �����մϴ�.";
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

            ExplainTextController gud = Instantiate(guide);
            gud.guide = "������ �����Ͽ����ϴ�.";
        }
        else
        {
            ExplainTextController gud = Instantiate(guide);
            gud.guide = "�ൿġ�� �����մϴ�.";
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
