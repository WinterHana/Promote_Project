using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// 대화창 UI 자체를 관리함
public class DialogManager : MonoBehaviour
{
    public List<TalkManager> talkManager;
    public TextMeshProUGUI talkText;
    public GameObject talkPanel;
    bool isAction;
    int talkIndex;

    public void Action(int objID, int dialogueID)
    {
        Talk(objID, dialogueID);
        talkPanel.SetActive(isAction);
    }

    public void exitAction()        // 대화창 종료
    {
        isAction = false;
        talkPanel.SetActive(isAction);
    }

    void Talk(int objID, int dialogueID)
    {
        string talkData = talkManager[objID].GetDialouge(dialogueID, talkIndex);

        // 대화 끝
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            return;
        }

        // 대화 내용이 더 있다면 실행한다.
        talkText.text = talkData;
        isAction = true;
        talkIndex++;
    }
}
