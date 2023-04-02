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
        op.allowSceneActivation = false;        // �ε� �ð��� �÷��� ����ũ �ε��� ����

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;

            // �ε� ���࿡ ���� �ε��ٸ� ä���.
            if (op.progress < 0.9f)
            {
                loadingBar.fillAmount = op.progress;
            }
            // 90% �̻� �Ϸᰡ �Ǹ� ����ũ �ε��� ���� 1�� ä�� ��, ���� �θ���.
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
