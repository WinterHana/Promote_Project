using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ClearPopUPManager : MonoBehaviour
{
    [Header("UI �ؽ�Ʈ �ֱ�")]
    public GameObject panel;
    public TextMeshProUGUI Title;
    public TextMeshProUGUI ClearTimeText;
    public TextMeshProUGUI RewardText;
    public TextMeshProUGUI ResultText;
    
    string clearTitle = "����!";
    string gameoverTitle = "����..";

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
        reminingTime = TimeManager.instance.setTime;

        StartCoroutine(delay());

        // �÷��̾� ������ ����
        PlayerMove.isMove = false;

        // �ƹ� Ű�� ������ ȨŸ������ �̵�
        if (Input.anyKeyDown && nextScene) GameManager.instance.goToHomeTown();

        // ���� ����
        int reward = GameManager.instance.moneyRewird();

        Title.text = clearTitle;

        ClearTimeText.text =
            "���� �ð� : " + (int) reminingTime + "��";
        RewardText.text =
            $"���̵� : {GameManager.instance.difficulty}\n"
            + $"�ܰ� : {GameManager.instance.step}\n"
            + $"ȹ���� �� : {reward}��";
        ResultText.text =
            $"��ǥ���� {PlayerStat.instance.maxMoney - PlayerStat.instance.money}�� ���ҽ��ϴ�.";

    }

    void gameOverPopUp()
    {
        panel.SetActive(true);
        
        StartCoroutine(delay());

        // �÷��̾� ������ ����
        PlayerMove.isMove = false;

        // �ƹ� Ű�� ������ ȨŸ������ �̵�
        if (Input.anyKeyDown && nextScene) GameManager.instance.goToHomeTown();

        Title.text = gameoverTitle;

        ClearTimeText.text =
            "���� ��ȸ��...";
        RewardText.text =
            $"���̵� : {GameManager.instance.difficulty}\n"
            + $"�ܰ� : {GameManager.instance.step}\n"
            + "ȹ���� �� : 0��";
        ResultText.text =
            $"��ǥ���� \n{PlayerStat.instance.maxMoney - PlayerStat.instance.money}�� ���ҽ��ϴ�.";
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
