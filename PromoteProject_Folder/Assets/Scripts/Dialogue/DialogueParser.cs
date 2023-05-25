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

    // 파일 이름을 입력받아 데이터를 파싱한다.
    public Dialogue[] Parse(string _FileName)
    {
        string getJSON = File.ReadAllText(_FileName);
        JsonData jsonData = JsonMapper.ToObject(getJSON);

        result = new Dialogue[jsonData.Count];                           // 결과 리스트
        lines = new List<string>();                                      // 임시 대화 내용 리스트

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
