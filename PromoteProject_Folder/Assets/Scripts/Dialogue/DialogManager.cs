using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// ��ȭâ UI ��ü�� ������
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

    public void exitAction()        // ��ȭâ ����
    {
        isAction = false;
        talkPanel.SetActive(isAction);
    }

    void Talk(int objID, int dialogueID)
    {
        string talkData = talkManager[objID].GetDialouge(dialogueID, talkIndex);

        // ��ȭ ��
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            return;
        }

        // ��ȭ ������ �� �ִٸ� �����Ѵ�.
        talkText.text = talkData;
        isAction = true;
        talkIndex++;
    }
}
