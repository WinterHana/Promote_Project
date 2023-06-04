using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [Header("남은 시간")]
    public float setTime;

    // 제한 시간이 지났는지 확인
    public bool isOverTime;

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
        // 메인화면, 엔딩 예외처리
        if (SceneManager.GetActiveScene().buildIndex is 0 or 9 or 10 or 11)
        {
            Destroy(gameObject);
        }
    }

    public void TodayControl()
    {
        // 홀수면 밤, 아니면 낮
        isNight = (PlayerStat.instance.times % 2 == 1) ? true : false;
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

            // 클리어할 때까지 시간 지나게 하기
            if(GameManager.instance.stageClaer == false) setTime -= Time.deltaTime;

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
