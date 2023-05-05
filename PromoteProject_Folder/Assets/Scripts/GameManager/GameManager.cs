using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// ���̵��� ���� �� �̵�
public enum BankScene
{
    Easy_Step1 = 1,
    Easy_Step2 = 2,
    Easy_Step3 = 3,
    Medium_Step1 = 4,
    Medium_Step2 = 5,
    Medium_Step3 = 6,
    Hard_Step1 = 7,
    Hard_Step2 = 8,
    Hard_Step3 = 9
}

public enum Difficulty
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}

public enum Step
{
    step1 = 1,
    step2 = 2,
    step3 = 3
}

public class GameManager : MonoBehaviour
{
    [Header("�̱��� ������Ʈ")]
    [SerializeField] TimeManager timeManager;
    [SerializeField] DataManager dataManager;
    [Space]
    [Header("���ѽð� ����")]
    public float setTimeEasy;
    public float setTimeMedium;
    public float setTimeHard;

    [Space]
    [Header("�������� Ŭ���� ���� Ȯ��")]
    public bool stageClaer;
    public bool isReward;

    [Space]
    [Header("���� ���̵��� �� ���� �ܰ�")]
    public Difficulty difficulty;
    public Step step;

    int sceneNum;               // �� ��ȣ
    public static GameManager instance; 
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ������ �ٲ�
    public void TodayChange()
    {
        if (!timeManager.isNight) {
            timeManager.isNight = true;
        }

        else {
            timeManager.isNight = false;
        }
    }

    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ʱ�ȭ
        stageClaer = false;
        isReward = false;
        sceneNum = SceneManager.GetActiveScene().buildIndex;

        // ���� ���̸� ������ ��, ���ѽð��� �����Ѵ�.
        if (sceneNum == (int)BankScene.Easy_Step1)
        {
            difficulty = Difficulty.Easy;
            step = Step.step1;
            TimeManager.instance.isStartTimeAttack();
            TimeManager.instance.setReminingTime(setTimeEasy);
        }
        else {
            TimeManager.instance.isEndTimeAttack();
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void goToHomeTown()
    {
        LoadingSceneController.LoadScene("HomeTown");
    }

    public int moneyRewird()
    {
        int reward = 10000 + (int)difficulty * (3000000 * PlayerStat.instance.intelligence + 10000000 * (int)step);
        if (!isReward)
        {
            PlayerStat.instance.money += reward;
            isReward = true;
        }
        return reward;
    }
}

