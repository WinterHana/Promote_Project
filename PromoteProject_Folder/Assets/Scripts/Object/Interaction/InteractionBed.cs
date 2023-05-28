using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBed : InteractionObject
{
    [SerializeField] int id;
    [SerializeField] FadeInOutController controller;            // FadeInOutCanvas 넣기
    [SerializeField] PlayerHPController playerHP;
    delegate void Action();

    private void Start()
    {
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
        Action action = changeTime;
    }

    public override void Interaction()
    {
        if (PlayerStat.instance.times % 2 == 0) id = 3002;          // 낮일 때, 밤을 맞이해야한다.
        else if (PlayerStat.instance.times % 2 == 1) id = 3001;     // 밤일 때, 낮을 맞이해야한다.

        SelectPopUpManager.instance.OpenPopUp(id);

        StartCoroutine(SelectCoroutine(changeTime));
    }

    void changeTime()
    {
        GameManager.instance.TodayChange();
        controller.ChangeDayAnim();
        // 침대에서 전환하면 체력 일부 회복
        PlayerStat.instance.health += 50;       
        if (PlayerStat.instance.health > PlayerStat.instance.maxHealth) PlayerStat.instance.health = PlayerStat.instance.maxHealth;
        playerHP.Hphealth.MyMaxValue = PlayerStat.instance.maxHealth;
        playerHP.Hphealth.MyCurrentValue = PlayerStat.instance.health;

        // 대화 수치 증가
        PlayerStat.instance.dialogue++;

        // 클리어 여부 확인
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
