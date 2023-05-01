using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// 플레이어 데이터 저장
public class SaveTest : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
        DataManager.instance.JsonLoad();
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 체인을 걸어서 이 함수는 매 씬마다 호출된다.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DataManager.instance.JsonSave();
        DataManager.instance.JsonLoad();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnApplicationQuit()
    {
        DataManager.instance.JsonSave();
    }
}
