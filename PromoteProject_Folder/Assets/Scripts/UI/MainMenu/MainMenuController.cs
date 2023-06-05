using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    public ExplainTextController con;
    public void startGame()
    {
        LoadingSceneController.LoadScene("Hometown");
    }
    public void Quit()
    {
        // DataManager.instance.JsonSave();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void init()
    {
        ExplainTextController instance = Instantiate(con);

        // 스탯 초기화
        PlayerStat.instance.exp = 0;
        PlayerStat.instance.money = 0;
        PlayerStat.instance.maxMoney = 100000000;
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

        // DataManager.instance.JsonSave();

        instance.guide = "초기화가 완료되었습니다.";
    }
}
