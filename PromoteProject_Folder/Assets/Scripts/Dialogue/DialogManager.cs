using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// 대화창 UI 자체를 관리함
/*
 * 이거 쓰는 법
 * 1. 대화창 텍스트, 판넬, 제목을 넣는다
 * 2. 오브젝트에 적용된 TalkManager를 List에 넣는다.
 * 3. TalkManager에 json파일 이름을 적어서 데이터를 가져온다.
 * 4. 이때 상호작용 키에 Action(objID, dialogueID);를 넣는다.
 * 5. 그럼 대화창이 뜬다.
 * 
 */
public class DialogManager : MonoBehaviour
{
    [Header("대화 내용을 가져올 오브젝트 삽입")]
    public List<TalkManager> talkManager;
    [Space]
    [Header("자식 오브젝트 삽입")]
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI talkName;
    public GameObject talkPanel;
    Animator panelAni;
    [Space]
    public bool endAction;      // 대화가 끝났음을 알려준다.

    bool isAction;
    int talkIndex;

    private void Start()
    {
        endAction = false;
        panelAni = talkPanel.GetComponent<Animator>();
    }

    public void Action(int objID, int dialogueID)
    {
        endAction = Talk(objID, dialogueID);
        panelAni.SetBool("isShow", isAction);
    }

    public void exitAction()        // 대화창 종료
    {
        isAction = false;
        talkIndex = 0;
        panelAni.SetBool("isShow", isAction);
    }

    // 대화 상호작용을 관여함
    bool Talk(int objID, int dialogueID)
    {
        talkName.text = talkManager[objID].GetName(dialogueID);
        string talkData = talkManager[objID].GetDialouge(dialogueID, talkIndex);
        Debug.Log(talkData);
        // 대화 끝
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            if (PlayerStat.instance.dialogue <= PlayerStat.instance.times) PlayerStat.instance.dialogue++;
            return true;
        }

        // 대화 내용이 더 있다면 실행한다.
        talkText.text = talkData;
        isAction = true;
        talkIndex++;

        return false;
    }
}
