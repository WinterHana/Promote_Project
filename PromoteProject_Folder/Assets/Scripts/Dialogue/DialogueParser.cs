using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

[System.Serializable]
public class DialogueParser : MonoBehaviour
{
    Dialogue tmp;
    List<Dialogue> result;
    List<string> lines;

    // ���� �̸��� �Է¹޾� �����͸� �Ľ��Ѵ�.
    public Dialogue[] Parse(string _FileName)
    {
        result = new List<Dialogue>();       // ��� ����Ʈ
        tmp = new Dialogue();
        lines = new List<string>();

        string getJSON = File.ReadAllText(_FileName);
        JsonData jsonData = JsonMapper.ToObject(getJSON);

        for (int i = 0; i < jsonData.Count; i++) {
            tmp.objname = jsonData[i]["objname"].ToString();
            tmp.id = (int) jsonData[i]["id"];

            for (int j = 0; j < jsonData[i]["lines"].Count; j++) {
                Debug.Log(jsonData[i]["lines"][j]["line"].ToString());
                lines.Add(jsonData[i]["lines"][j]["line"].ToString());
            }

            tmp.lines = lines.ToArray();
            lines.Clear();

            result.Add(tmp);
        }

        return result.ToArray();
    }
}
