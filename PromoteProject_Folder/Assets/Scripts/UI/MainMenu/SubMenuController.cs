using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuController : MonoBehaviour
{
    // public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public bool isMenu = false;

    private void Start()
    {
        // 시작할 때 메뉴 닫기
        isMenu = false;
        pauseMenu.SetActive(isMenu);
    }

    public void Menu()
    {
        isMenu = !isMenu;
        pauseMenu.SetActive(isMenu);
    }

    public void MainMenu()
    {
        LoadingSceneController.LoadScene("MainMenu");
    }

    public void Quit()
    {   
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    
}
