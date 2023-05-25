using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

[System.Serializable]
public class DialogueParser : MonoBehaviour
{
    Dialogue[] result;
    List<string> lines;

    // ���� �̸��� �Է¹޾� �����͸� �Ľ��Ѵ�.
    public Dialogue[] Parse(string _FileName)
    {
        string getJSON = File.ReadAllText(_FileName);
        JsonData jsonData = JsonMapper.ToObject(getJSON);

        result = new Dialogue[jsonData.Count];                           // ��� ����Ʈ
        lines = new List<string>();                                      // �ӽ� ��ȭ ���� ����Ʈ

        // Debug.Log(result.Length);

        for (int i = 0; i < jsonData.Count; i++) {
            result[i] = new Dialogue();
            result[i].objname = jsonData[i]["objname"].ToString();
            result[i].id = (int) jsonData[i]["id"];

            for (int j = 0; j < jsonData[i]["lines"].Count; j++) {
                lines.Add(jsonData[i]["lines"][j]["line"].ToString());
            }

            result[i].lines = lines.ToArray();

            lines.Clear();
        }

        return result;
    }
}
