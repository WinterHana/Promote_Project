using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public enum BankScene
{
    Easy = 0,
    Medium = 1,
    Hard = 2
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

    // 낮밤을 바꿈
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
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 변수 초기화
        stageClaer = false;
        // 은행 털이를 시작할 때, 제한시간을 설정한다.
        if (SceneManager.GetActiveScene().buildIndex == (int)BankScene.Easy)
        {
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
}

