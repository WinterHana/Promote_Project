using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoveScene : MonoBehaviour
{
    [SerializeField] string nextScene;
    bool upArrow;

    Dictionary<int, string> dic;    // �˾�â�� �� ���� �Ľ��ؼ� ����
    public int dialogueNum;         // ���� â�� ���� ��ȭ���� ����

    private void Start()
    {
        dic = SelectPopParser.parser();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) 
            upArrow = true;
        else
            upArrow = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // ���� ���� ������ �� ����ó�� : �˾�â�� �߰� �Ѵ�.
        if (this.CompareTag("MapSelect"))
        {
            if (collision.CompareTag("Player") && upArrow)
            {
                SelectPopUpManager.instance.OpenPopUp(dialogueNum);
                StartCoroutine(SelectCoroutine());
            }
        }

        else if (collision.CompareTag("Player") && upArrow) {
            LoadingSceneController.LoadScene(nextScene);
        }
    }

    IEnumerator SelectCoroutine()
    {
        yield return new WaitUntil(() => !SelectPopUpManager.instance.isSelect);

        if (SelectPopUpManager.instance.sign)
        {
            LoadingSceneController.LoadScene(nextScene);
        }
    }
}
