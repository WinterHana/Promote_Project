using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


// ��ȭ �����͸� ������
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

    // ��ȭ ���� ��������
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
