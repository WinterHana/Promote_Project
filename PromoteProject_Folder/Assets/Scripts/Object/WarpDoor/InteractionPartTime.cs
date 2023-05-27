using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPartTime : InteractionObject
{
    delegate void Action();
    [SerializeField] FadeInOutController controller;            // FadeInOutCanvas ³Φ±β

    private void Start()
    {
        Action action = changeTime;
    }
    public override void Interaction()
    {
        SelectPopUpManager.instance.OpenPopUp(1004);

        StartCoroutine(SelectCoroutine(changeTime));
    }

    IEnumerator SelectCoroutine(Action action)
    {
        yield return new WaitUntil(() => !SelectPopUpManager.instance.isSelect);

        if (SelectPopUpManager.instance.sign)
        {
            action();
        }
    }

    void changeTime()
    {
        GameManager.instance.TodayChange();
        PlayerStat.instance.money += 5000000;       // µ· Αυ°΅
        controller.ChangeDayAnim();
    }
}
