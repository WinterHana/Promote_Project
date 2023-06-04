using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [Header("�ð� ��Ʈ�� ����")]
    public bool isNight;
    public bool isTimeAttack;
    [Space]
    [Header("�ڽ� ������Ʈ ����")]
    public TextMeshProUGUI TodayText;
    public TextMeshProUGUI TimeRemining;
    public Image TimeBackGround;
    public Image sun;
    public Image moon;


    // ���� �ð� ����
    [Header("���� �ð�")]
    public float setTime;

    // ���� �ð��� �������� Ȯ��
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
        // ����ȭ��, ���� ����ó��
        if (SceneManager.GetActiveScene().buildIndex is 0 or 9 or 10 or 11)
        {
            Destroy(gameObject);
        }
    }

    public void TodayControl()
    {
        // Ȧ���� ��, �ƴϸ� ��
        isNight = (PlayerStat.instance.times % 2 == 1) ? true : false;
        if (isNight)
        {
            sun.enabled = false;
            moon.enabled = true;
            TodayText.text = "��";
        }

        else if(!isNight)
        {
            sun.enabled = true;
            moon.enabled = false;
            TodayText.text = "��";
        }
    }

    void TimeAttackStarter()
    {
        if (isTimeAttack)
        {
            TimeBackGround.enabled = true;
            TimeRemining.enabled = true;
            
            TimeRemining.text = "���� �ð� : " + (int)setTime + "��";

            // Ŭ������ ������ �ð� ������ �ϱ�
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
