using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �÷��̾� ������ ����
public class SaveTest : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
        DataManager.instance.JsonLoad();
    }

    void OnEnable()
    {
        // �� �Ŵ����� sceneLoaded�� ü���� �Ǵ�.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
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
