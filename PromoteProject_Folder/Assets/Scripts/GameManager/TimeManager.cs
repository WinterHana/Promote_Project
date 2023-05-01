using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    [Header("시간 컨트롤 변수")]
    public bool isNight;
    public bool isTimeAttack;
    [Space]
    [Header("자식 오브젝트 삽입")]
    public TextMeshProUGUI TodayText;
    public TextMeshProUGUI TimeRemining;
    public Image TimeBackGround;
    public Image sun;
    public Image moon;


    // 제한 시간 설정
    float setTime;

    // 제한 시간이 지났는지 확인
    bool isOverTime;

    public static TimeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        TodayControl();
        TimeAttackStarter();
    }


    void TodayControl()
    {
        if (isNight)
        {
            sun.enabled = false;
            moon.enabled = true;
            TodayText.text = "밤";
        }

        else if(!isNight)
        {
            sun.enabled = true;
            moon.enabled = false;
            TodayText.text = "낮";
        }
    }

    void TimeAttackStarter()
    {
        if (isTimeAttack)
        {
            TimeBackGround.enabled = true;
            TimeRemining.enabled = true;
            
            TimeRemining.text = "남은 시간 : " + (int)setTime + "초";

            setTime -= Time.deltaTime;

            if (setTime < 0f) {
                isOverTime = true;
                setTime = 0f;
            }
        }
        else
        {
            TimeBackGround.enabled = false;
            TimeRemining.enabled = false;
            isOverTime = false;
        }
    }

    public void isStartTimeAttack()
    {
        isTimeAttack = true;
    }

    public void isEndTimeAttack()
    {
        isTimeAttack = false;
    }

    public void setReminingTime(float time)
    {
        setTime = time;
    }
}
