using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverCanvasController : MonoBehaviour
{
    [Header("��� �ؽ�Ʈ ����")]
    public TextMeshProUGUI result_1;
    public TextMeshProUGUI result_2;

    private void Start()
    {
        result_1.text = $"���� �� : { PlayerStat.instance.money }��";
        result_2.text = $"��ǥ�� �� : { PlayerStat.instance.maxMoney }��";
    }

    public void MainMeun()
    {
        // ���� �ʱ�ȭ
        PlayerStat.instance.exp = 0;
        PlayerStat.instance.money = 0;
        PlayerStat.instance.maxMoney = 50000000;
        PlayerStat.instance.endurance = 0;
        PlayerStat.instance.strength = 0;
        PlayerStat.instance.intelligence = 0;
        PlayerStat.instance.atkDamege = 10;
        PlayerStat.instance.health = 200;
        PlayerStat.instance.maxHealth = 200;
        PlayerStat.instance.working = 50;
        PlayerStat.instance.maxWorking = 50;
        PlayerStat.instance.times = 0;
        PlayerStat.instance.dialogue = 0;

        // ���� �޴��� ���ư���
        LoadingSceneController.LoadScene("MainMenu");
    }
}
