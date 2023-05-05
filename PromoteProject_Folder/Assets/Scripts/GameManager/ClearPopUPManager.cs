using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ClearPopUPManager : MonoBehaviour
{
    [Header("UI 텍스트 넣기")]
    public GameObject panel;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI ClearTimeText;
    public TextMeshProUGUI RewardText;
    public TextMeshProUGUI ResultText;
    
    string clearTitle = "성공!";
    string gameoverTitle = "실패..";

    float reminingTime;
    bool gameClear;
    bool nextScene;

    private void Start()
    {
        panel.SetActive(false);
        gameClear = false;
        nextScene = false;
    }

    private void Update()
    {
        if (TimeManager.instance.isOverTime && !gameClear)                 // 실패
        {
            gameOverPopUp();
        }
        else if (GameManager.instance.stageClaer == true)   // 성공
        {
            stageClearPopUp();
            gameClear = true;
        }   
        else                                                // 그 외의 것
        {
            closePopUp();
        }
    }

    void stageClearPopUp()
    {
        panel.SetActive(true);
        reminingTime = TimeManager.instance.setTime;

        StartCoroutine(delay());

        // 플레이어 움직임 봉인
        PlayerMove.isMove = false;

        // 아무 키나 누르면 홈타운으로 이동
        if (Input.anyKeyDown && nextScene) GameManager.instance.goToHomeTown();

        // 보상 제공
        int reward = GameManager.instance.moneyRewird();

        Title.text = clearTitle;

        ClearTimeText.text =
            "남은 시간 : " + (int) reminingTime + "초";
        RewardText.text =
            $"난이도 : {GameManager.instance.difficulty}\n"
            + $"단계 : {GameManager.instance.step}\n"
            + $"획득한 돈 : {reward}원";
        ResultText.text =
            $"목표까지 {PlayerStat.instance.maxMoney - PlayerStat.instance.money}원 남았습니다.";

    }

    void gameOverPopUp()
    {
        panel.SetActive(true);
        
        StartCoroutine(delay());

        // 플레이어 움직임 봉인
        PlayerMove.isMove = false;

        // 아무 키나 누르면 홈타운으로 이동
        if (Input.anyKeyDown && nextScene) GameManager.instance.goToHomeTown();

        Title.text = gameoverTitle;

        ClearTimeText.text =
            "다음 기회에...";
        RewardText.text =
            $"난이도 : {GameManager.instance.difficulty}\n"
            + $"단계 : {GameManager.instance.step}\n"
            + "획득한 돈 : 0원";
        ResultText.text =
            $"목표까지 \n{PlayerStat.instance.maxMoney - PlayerStat.instance.money}원 남았습니다.";
    }

    void closePopUp()
    {
        panel.SetActive(false);
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2.0f);
        nextScene = true;
    }
}
