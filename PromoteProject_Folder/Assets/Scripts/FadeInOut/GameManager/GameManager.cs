using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// ���̵��� ���� �� �̵�
public enum BankScene
{
    Easy_Step1 = 3,
    Medium_Step1 = 4,
    Hard_Step1 = 5,
    Easy_Step2 = 2,
    Easy_Step3 = 3,
    Medium_Step2 = 5,
    Medium_Step3 = 6,
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

    private void Start()
    {
        TodayCheck();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }
    // ������ �ٲ�
    public void TodayChange()
    {
        PlayerStat.instance.times += 1;

        // ¦���� ���� ��, Ȧ���� ���� ���̴�.
        if (PlayerStat.instance.times % 2 == 1) {
            timeManager.isNight = true;
        }

        else {
            timeManager.isNight = false;
        }
    }

    // ���� �ð��� üũ���ش�. -> start�� �־��ش�.
    void TodayCheck()
    {
        // ¦���� ���� ��, Ȧ���� ���� ���̴�.
        if (PlayerStat.instance.times % 2 == 1)
        {
            timeManager.isNight = true;
        }

        else
        {
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
        else if (sceneNum == (int)BankScene.Medium_Step1)
        {
            difficulty = Difficulty.Medium;
            step = Step.step1;
            TimeManager.instance.isStartTimeAttack();
            TimeManager.instance.setReminingTime(setTimeMedium);
        }
        else if (sceneNum == (int)BankScene.Hard_Step1)
        {
            difficulty = Difficulty.Hard;
            step = Step.step1;
            TimeManager.instance.isStartTimeAttack();
            TimeManager.instance.setReminingTime(setTimeHard);
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
        int reward = (int)difficulty * 
            (1000000 * PlayerStat.instance.intelligence 
            + 100000 * (int) TimeManager.instance.setTime 
            + 5000000 * (int)step);
        if (!isReward)
        {
            PlayerStat.instance.money += reward;
            isReward = true;
        }
        return reward;
    }
}

