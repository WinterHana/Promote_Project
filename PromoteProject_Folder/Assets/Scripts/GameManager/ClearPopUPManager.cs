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

    string clearTitle = "����!";
    string gameoverTitle = "����..";

    float reminingTime;
    bool gameClear;             // �ߺ� ���� ����
    private void Start()
    {
        panel.SetActive(false);
        gameClear = false;

    }

    private void Update()
    {

        if (TimeManager.instance.isOverTime && !gameClear)                 // ����
        {
            gameOverPopUp();
        }
        else if (GameManager.instance.stageClaer == true)   // ����
        {
            stageClearPopUp();
            gameClear = true;
        }   
        else                                                // �� ���� ��
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
            "���� �ð� : " + (int) reminingTime + "��";

        PlayerMove.isMove = false;
    }

    void gameOverPopUp()
    {
        panel.SetActive(true);
        Title.text = gameoverTitle;

        contents.text =
            "���� ��ȸ��...";

        PlayerMove.isMove = false;

        // �ƹ� Ű�� ������ ȨŸ������ �̵�
        if (Input.anyKeyDown) GameManager.instance.goToHomeTown();        
    }

    void closePopUp()
    {
        panel.SetActive(false);
    }
}
