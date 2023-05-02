using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClearPopUPManager : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI contents;
    public GameObject panel;

    string clearTitle = "성공!";
    string gameoverTitle = "실패..";

    float reminingTime;
    bool gameClear;             // 중복 접근 방지
    private void Start()
    {
        panel.SetActive(false);
        gameClear = false;

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
        Title.text = clearTitle;
        reminingTime = TimeManager.instance.setTime;

        contents.text =
            "남은 시간 : " + (int) reminingTime + "초";

        PlayerMove.isMove = false;
    }

    void gameOverPopUp()
    {
        panel.SetActive(true);
        Title.text = gameoverTitle;

        contents.text =
            "다음 기회에...";

        PlayerMove.isMove = false;

        // 아무 키나 누르면 홈타운으로 이동
        if (Input.anyKeyDown) GameManager.instance.goToHomeTown();        
    }

    void closePopUp()
    {
        panel.SetActive(false);
    }
}
