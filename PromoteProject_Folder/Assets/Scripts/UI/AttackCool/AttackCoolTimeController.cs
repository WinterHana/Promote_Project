using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttackCoolTimeController : MonoBehaviour
{
    public TextMeshProUGUI text_CoolTime;
    public Image image_fill;
    public float time_cooltime;
    private float time_current;
    private float time_start;
    private bool isEnded = true;
    PlayerMove player;
    void Start()
    {
        Init_UI();
        Trigger_Skill();
    }

    void Update()
    {
        if (isEnded)
            return;
        Check_CoolTime();
    }

    private void Init_UI()
    {
        image_fill.type = Image.Type.Filled;
        image_fill.fillMethod = Image.FillMethod.Radial360;
        image_fill.fillOrigin = (int)Image.Origin360.Top;
        image_fill.fillClockwise = false;
    }

    private void Check_CoolTime()
    {
        time_current = Time.time - time_start;
        if (time_current < time_cooltime)
        {
            Set_FillAmount(time_cooltime - time_current);
        }
        else if (!isEnded)
        {
            End_CoolTime();
        }
    }

    private void End_CoolTime()
    {
        Set_FillAmount(0);
        isEnded = true;
        text_CoolTime.gameObject.SetActive(false);
        // Debug.Log("Skills Available!");
    }

    public void Trigger_Skill()
    {
        if (!isEnded)
        {
           //  Debug.LogError("Hold On");
            return;
        }

        Reset_CoolTime();
    }

    private void Reset_CoolTime()
    {
        text_CoolTime.gameObject.SetActive(true);
        time_current = time_cooltime;
        time_start = Time.time;
        Set_FillAmount(time_cooltime);
        isEnded = false;
    }

    private void Set_FillAmount(float _value)
    {
        image_fill.fillAmount = _value / time_cooltime;
        string txt = _value.ToString("0.0");
        text_CoolTime.text = txt;
        // Debug.Log(txt);
    }
}
