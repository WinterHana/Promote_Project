using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeInOutController : MonoBehaviour
{
    [Header("�⺻ ����")]
    public float FadeTime = 1f; // Fadeȿ�� ����ð�
    public Image fadeImg;
    public TextMeshProUGUI fadeText;
    float start;
    float end;
    float time = 0f;
    bool isPlaying = false;

    // ���� ���
    int day;
    string isNight;

    void calDay()
    {
        day = PlayerStat.instance.times / 2 + 1;
        isNight = (PlayerStat.instance.times % 2 == 0) ? "��" : "��";
    }

    void Start()
    {
        OutStartFadeAnim();
    }

    public void ChangeDayAnim()
    {
        if (isPlaying == true) //�ߺ��������
        {
            return;
        }
        // �ؽ�Ʈ ����
        calDay();
        fadeText.text = day + "���� " + isNight;
        StartCoroutine("sum");

    }
    public void OutStartFadeAnim()
    {
        if (isPlaying == true) //�ߺ��������
        {
            return;
        }
        start = 1f;
        end = 0f;
        // �ؽ�Ʈ ����
        calDay();
        fadeText.text = day + "���� " + isNight;
        StartCoroutine("fadeoutplay");    //�ڷ�ƾ ����
    }

    public void InStartFadeAnim()
    {
        if (isPlaying == true) //�ߺ��������
        {
            return;
        }
        start = 0f;
        end = 1f;
        // �ؽ�Ʈ ����
        calDay();
        fadeText.text = day + "���� " + isNight;
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
        // Debug.Log("���� ��");
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

        // Debug.Log("���� �� 2");
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
