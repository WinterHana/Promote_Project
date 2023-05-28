using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMoveScene : MonoBehaviour
{
    [SerializeField] string nextScene;
    bool upArrow;

    Dictionary<int, string> dic;    // 팝업창에 쓸 내용 파싱해서 저장
    public int dialogueNum;         // 선택 창에 따른 대화내용 설정
    int result;

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
        // 은행 맵을 선택할 때 예외처리 : 팝업창이 뜨게 한다.
        if (this.CompareTag("MapSelect"))
        {
            if (collision.CompareTag("Player") && upArrow)
            {
                if (PlayerStat.instance.times % 2 == 0) result = 4001;
                else result = dialogueNum;

                SelectPopUpManager.instance.OpenPopUp(result);
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

        if (SelectPopUpManager.instance.sign && result != 4001)
        {
            LoadingSceneController.LoadScene(nextScene);
        }
    }
}
