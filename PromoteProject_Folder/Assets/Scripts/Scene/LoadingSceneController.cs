using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    static string nextScene;
    [SerializeField] Image loadingBar;

    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    void Start()
    {
        StartCoroutine(LoadingSceneProcess());
    }


    IEnumerator LoadingSceneProcess()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;        // 로딩 시간을 늘려서 페이크 로딩을 만듬

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            // 로딩 진행에 따라서 로딩바를 채운다.
            if (op.progress < 0.9f)
            {
                loadingBar.fillAmount = op.progress;
            }
            // 90% 이상 완료가 되면 페이크 로딩을 통해 1초 채운 뒤, 씬을 부른다.
            else
            {
                timer += Time.unscaledDeltaTime;
                loadingBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (loadingBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
