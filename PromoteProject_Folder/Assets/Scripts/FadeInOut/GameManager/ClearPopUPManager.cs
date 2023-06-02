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
        else if (PlayerStat.instance.health <= 0)
        {
            gameOverPopUp();
        }
        else if (GameManager.instance.stageClaer == true)                   // ����
        {
            stageClearPopUp();
            gameClear = true;
        }
        else                                                                // �� ���� ��
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
        if (Input.anyKeyDown && nextScene) {
            GameManager.instance.goToHomeTown();
            PlayerStat.instance.times++;
            PlayerStat.instance.working = PlayerStat.instance.maxWorking;       // �ൿġ ȸ��
        }
        

        // ���� ����
        int reward = GameManager.instance.moneyRewird();

        Title.text = clearTitle;

        int targetMoney = PlayerStat.instance.maxMoney - PlayerStat.instance.money;
        if (targetMoney <= 0) targetMoney = 0;

        ClearTimeText.text =
            "���� �ð� : " + (int) reminingTime + "��";
        RewardText.text =
            $"���̵� : {GameManager.instance.difficulty}\n"
            + $"�ܰ� : {GameManager.instance.step}\n"
            + $"ȹ���� �� : {reward}��";
        ResultText.text =
            $"��ǥ���� {targetMoney}��\n���ҽ��ϴ�.";

    }

    void gameOverPopUp()
    {
        panel.SetActive(true);
        
        StartCoroutine(delay());

        // �÷��̾� ������ ����
        PlayerMove.isMove = false;

        // �ƹ� Ű�� ������ ȨŸ������ �̵�
        if (Input.anyKeyDown && nextScene) {
            GameManager.instance.goToHomeTown();
            if (PlayerStat.instance.health <= 0) PlayerStat.instance.health = PlayerStat.instance.maxHealth / 2;     // ������ ü���� �� ���� ȸ��
            PlayerStat.instance.times++;
            PlayerStat.instance.dialogue++;
            PlayerStat.instance.working = PlayerStat.instance.maxWorking;   // �ൿġ ȸ�� 


        }
        
        Title.text = gameoverTitle;

        int targetMoney = PlayerStat.instance.maxMoney - PlayerStat.instance.money;
        if (targetMoney <= 0) targetMoney = 0;

        ClearTimeText.text =
            "���� ��ȸ��...";
        RewardText.text =
            $"���̵� : {GameManager.instance.difficulty}\n"
            + $"�ܰ� : {GameManager.instance.step}\n"
            + "ȹ���� �� : 0��";
        ResultText.text =
            $"��ǥ���� {targetMoney}��\n���ҽ��ϴ�.";
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
