using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


// ��ȭâ UI ��ü�� ������
/*
 * �̰� ���� ��
 * 1. ��ȭâ �ؽ�Ʈ, �ǳ�, ������ �ִ´�
 * 2. ������Ʈ�� ����� TalkManager�� List�� �ִ´�.
 * 3. TalkManager�� json���� �̸��� ��� �����͸� �����´�.
 * 4. �̶� ��ȣ�ۿ� Ű�� Action(objID, dialogueID);�� �ִ´�.
 * 5. �׷� ��ȭâ�� ���.
 * 
 */
public class DialogManager : MonoBehaviour
{
    [Header("��ȭ ������ ������ ������Ʈ ����")]
    public List<TalkManager> talkManager;
    [Space]
    [Header("�ڽ� ������Ʈ ����")]
    public TextMeshProUGUI talkText;
    public TextMeshProUGUI talkName;
    public GameObject talkPanel;
    Animator panelAni;
    [Space]
    public bool endAction;      // ��ȭ�� �������� �˷��ش�.

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

    public void exitAction()        // ��ȭâ ����
    {
        isAction = false;
        talkIndex = 0;
        panelAni.SetBool("isShow", isAction);
    }

    // ��ȭ ��ȣ�ۿ��� ������
    bool Talk(int objID, int dialogueID)
    {
        talkName.text = talkManager[objID].GetName(dialogueID);
        string talkData = talkManager[objID].GetDialouge(dialogueID, talkIndex);
        Debug.Log(talkData);
        // ��ȭ ��
        if (talkData == null) {
            isAction = false;
            talkIndex = 0;
            if (PlayerStat.instance.dialogue <= PlayerStat.instance.times) PlayerStat.instance.dialogue++;
            return true;
        }

        // ��ȭ ������ �� �ִٸ� �����Ѵ�.
        talkText.text = talkData;
        isAction = true;
        talkIndex++;

        return false;
    }
}
