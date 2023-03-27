using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    public static MoveScene moveScene;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SceneManager.GetActiveScene().name == "TestScene_1") SceneManager.LoadScene("TestScene_2");
            else SceneManager.LoadScene("TestScene_1");
        }
    }
}
