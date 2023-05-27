using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

// 난이도에 따른 맵 이동
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
    [Header("싱글톤 오브젝트")]
    [SerializeField] TimeManager timeManager;
    [SerializeField] DataManager dataManager;
    [Space]
    [Header("제한시간 설정")]
    public float setTimeEasy;
    public float setTimeMedium;
    public float setTimeHard;

    [Space]
    [Header("스테이지 클리어 여부 확인")]
    public bool stageClaer;
    public bool isReward;

    [Space]
    [Header("맵의 난이도와 그 내의 단계")]
    public Difficulty difficulty;
    public Step step;

    int sceneNum;               // 씬 번호
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
    // 낮밤을 바꿈
    public void TodayChange()
    {
        PlayerStat.instance.times += 1;

        // 짝수일 때는 낮, 홀수일 때는 밤이다.
        if (PlayerStat.instance.times % 2 == 1) {
            timeManager.isNight = true;
        }

        else {
            timeManager.isNight = false;
        }
    }

    // 지금 시각을 체크해준다. -> start에 넣어준다.
    void TodayCheck()
    {
        // 짝수일 때는 밤, 홀수일 때는 낮이다.
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
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 변수 초기화
        stageClaer = false;
        isReward = false;
        sceneNum = SceneManager.GetActiveScene().buildIndex;

        // 은행 털이를 시작할 때, 제한시간을 설정한다.
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

