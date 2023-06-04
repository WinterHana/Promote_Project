using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPartTime : InteractionObject
{
    delegate void Action();
    [SerializeField] FadeInOutController controller;            // FadeInOutCanvas 넣기
    [SerializeField] PlayerHPController playerHP;

    private void Start()
    {
        Action[] action = { changeTime, changeEnding };
        playerHP = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerHPController>();
    }
    public override void Interaction()
    {
        PlayerMove.isMove = false;
        if (PlayerStat.instance.times % 2 == 0)
        {
            SelectPopUpManager.instance.OpenPopUp(4001);
            PlayerMove.isMove = true;
        }
        else if (PlayerStat.instance.dialogue == 13)
        {
            // 히든 엔딩
            SelectPopUpManager.instance.OpenPopUp(2);
            StartCoroutine(SelectCoroutine(changeEnding));
        }
        else
        {
            SelectPopUpManager.instance.OpenPopUp(1004);
            StartCoroutine(SelectCoroutine(changeTime));
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

    void changeTime()
    {
        GameManager.instance.TodayChange();
        PlayerStat.instance.money += 7000000;       // 돈 증가
        PlayerStat.instance.working = PlayerStat.instance.maxWorking;         // 행동치 회복
        playerHP.Hpworking.MyCurrentValue = PlayerStat.instance.working;
        controller.ChangeDayAnim();                 // 시간 전환

        // 클리어 여부 확인
        GameManager.instance.isClear();
        if (GameManager.instance.gameClear) GameManager.instance.gameClearCheck();
        else if((PlayerStat.instance.times >= GameManager.instance.checkClearTime)) GameManager.instance.gameClearCheck();
    }

    void changeEnding()
    {
        LoadingSceneController.LoadScene("TrueEnding");
    }
}
