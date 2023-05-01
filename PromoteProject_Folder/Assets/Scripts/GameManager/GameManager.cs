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
    [Header("�̱��� ������Ʈ")]
    [SerializeField] TimeManager timeManager;
    [SerializeField] DataManager dataManager;
    [Space]
    [Header("���ѽð� ����")]
    public float setTimeEasy;
    public float setTimeMedium;
    public float setTimeHard;

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
        // ���� ���̸� ������ ��, ���ѽð��� �����Ѵ�.
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

}

