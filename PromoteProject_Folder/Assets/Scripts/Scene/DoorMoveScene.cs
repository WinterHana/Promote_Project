using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoveScene : MonoBehaviour
{
    [SerializeField] string nextScene;
    bool upArrow;
    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
            upArrow = true;
        else
            upArrow = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && upArrow) {
            LoadingSceneController.LoadScene(nextScene);
        }
    }
}
