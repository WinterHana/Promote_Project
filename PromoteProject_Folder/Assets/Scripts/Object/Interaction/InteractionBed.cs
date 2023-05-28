using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBed : InteractionObject
{
    [SerializeField] int id;
    [SerializeField] FadeInOutController controller;            // FadeInOutCanvas �ֱ�
    [SerializeField] PlayerHPController playerHP;
    delegate void Action();

    private void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
        Action action = changeTime;
    }

    public override void Interaction()
    {
        if (PlayerStat.instance.times % 2 == 0) id = 3002;          // ���� ��, ���� �����ؾ��Ѵ�.
        else if (PlayerStat.instance.times % 2 == 1) id = 3001;     // ���� ��, ���� �����ؾ��Ѵ�.

        SelectPopUpManager.instance.OpenPopUp(id);

        StartCoroutine(SelectCoroutine(changeTime));
    }

    void changeTime()
    {
        GameManager.instance.TodayChange();
        controller.ChangeDayAnim();
        // ħ�뿡�� ��ȯ�ϸ� ü�� �Ϻ� ȸ��
        PlayerStat.instance.health += 50;       
        if (PlayerStat.instance.health > PlayerStat.instance.maxHealth) PlayerStat.instance.health = PlayerStat.instance.maxHealth;
        playerHP.Hphealth.MyMaxValue = PlayerStat.instance.maxHealth;
        playerHP.Hphealth.MyCurrentValue = PlayerStat.instance.health;

        // ��ȭ ��ġ ����
        PlayerStat.instance.dialogue++;

        // Ŭ���� ���� Ȯ��
        GameManager.instance.isClear();
        if (GameManager.instance.gameClear) GameManager.instance.gameClearCheck();
    }

    IEnumerator SelectCoroutine(Action action)
    {
        yield return new WaitUntil(() => !SelectPopUpManager.instance.isSelect);

        if (SelectPopUpManager.instance.sign)
        {
            action();
        }
    }
}
