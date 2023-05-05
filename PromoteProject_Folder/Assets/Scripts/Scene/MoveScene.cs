using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveScene : MonoBehaviour
{
    [SerializeField] string nextScene;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadingSceneController.LoadScene(nextScene);
        }
    }
}
