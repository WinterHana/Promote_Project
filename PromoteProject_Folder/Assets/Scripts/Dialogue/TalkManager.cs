using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


// 대화 데이터를 관리함
[System.Serializable]
public class TalkManager : MonoBehaviour
{
    public string fileName;
    string path;

    Dialogue[] dialogues;


    private void Awake()
    {
        initDialogue();
    }

    // 대화 내용 가져오기
    void initDialogue()
    {
        DialogueParser parser = FindObjectOfType<DialogueParser>();

        path = Path.Combine(Application.dataPath + "/Resources/" + fileName + ".json");

        dialogues = parser.Parse(path);
    }

    public string GetDialouge(int id, int line)
    {
        if (line == dialogues[id].lines.Length)
            return null;
        else
            return dialogues[id].lines[line];
    }

    public string GetName(int id)
    {
        return dialogues[id].objname;
    }
}
