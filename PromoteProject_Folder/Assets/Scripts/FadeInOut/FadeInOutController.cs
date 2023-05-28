using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeInOutController : MonoBehaviour
{
    [Header("기본 설정")]
    public float FadeTime = 1f; // Fade효과 재생시간
    public Image fadeImg;
    public TextMeshProUGUI fadeText;
    float start;
    float end;
    float time = 0f;
    bool isPlaying = false;

    // 요일 계산
    int day;
    string isNight;

    void calDay()
    {
        day = PlayerStat.instance.times / 2 + 1;
        isNight = (PlayerStat.instance.times % 2 == 0) ? "낮" : "밤";
    }

    void Start()
    {
        OutStartFadeAnim();
    }

    public void ChangeDayAnim()
    {
        if (isPlaying == true) //중복재생방지
        {
            return;
        }
        // 텍스트 수정
        calDay();
        fadeText.text = day + "일차 " + isNight;
        StartCoroutine("sum");

    }
    public void OutStartFadeAnim()
    {
        if (isPlaying == true) //중복재생방지
        {
            return;
        }
        start = 1f;
        end = 0f;
        // 텍스트 수정
        calDay();
        fadeText.text = day + "일차 " + isNight;
        StartCoroutine("fadeoutplay");    //코루틴 실행
    }

    public void InStartFadeAnim()
    {
        if (isPlaying == true) //중복재생방지
        {
            return;
        }
        start = 0f;
        end = 1f;
        // 텍스트 수정
        calDay();
        fadeText.text = day + "일차 " + isNight;
        StartCoroutine("fadeinplay");
    }

    IEnumerator fadeoutplay()
    {
        isPlaying = true;

        Color fadecolor = fadeImg.color;
        Color textcolor = fadeText.color;
        time = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);
        textcolor.a = Mathf.Lerp(start, end, time);

        while (fadecolor.a > 0f)
        {
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);
            textcolor.a = Mathf.Lerp(start, end, time);
            fadeImg.color = fadecolor;
            fadeText.color = textcolor;
            yield return null;
        }

        isPlaying = false;

    }

    IEnumerator fadeinplay()
    {
        isPlaying = true;

        Color fadecolor = fadeImg.color;
        Color textcolor = fadeText.color;
        time = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);
        textcolor.a = Mathf.Lerp(start, end, time);

        while (fadecolor.a < 1f)
        {
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);
            textcolor.a = Mathf.Lerp(start, end, time);
            fadeImg.color = fadecolor;
            fadeText.color = textcolor;
            yield return null;
        }
        isPlaying = false;
    }
    IEnumerator sum()
    {
        // Debug.Log("실행 중");
        isPlaying = true;
        
        // FadeIn
        time = 0f;
        start = 0f;
        end = 1f;
        Color fadecolor = fadeImg.color;
        Color textcolor = fadeText.color;
        fadecolor.a = Mathf.Lerp(start, end, time);
        textcolor.a = Mathf.Lerp(start, end, time);

        while (fadecolor.a < 1f)
        {
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);
            textcolor.a = Mathf.Lerp(start, end, time);
            fadeImg.color = fadecolor;
            fadeText.color = textcolor;
            yield return null;
        }

        // Debug.Log("실행 중 2");
        // FadeOut
        time = 0f;
        start = 1f;
        end = 0f;
        fadecolor.a = Mathf.Lerp(start, end, time);
        textcolor.a = Mathf.Lerp(start, end, time);
        while (fadecolor.a > 0f)
        {
            time += Time.deltaTime / FadeTime;
            fadecolor.a = Mathf.Lerp(start, end, time);
            textcolor.a = Mathf.Lerp(start, end, time);
            fadeImg.color = fadecolor;
            fadeText.color = textcolor;
            yield return null;
        }

        isPlaying = false;
    }
}
