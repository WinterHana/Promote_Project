using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class SelectPopParser : MonoBehaviour
{
    public static Dictionary<int, string> parser()
    {
        Dictionary<int, string> dic;
        dic = new Dictionary<int, string>();

        string JsonString = File.ReadAllText(Application.dataPath + "/Resources/SelectPopUpDialogue.json");
        JsonData jsonData = JsonMapper.ToObject(JsonString);

        for (int i = 0; i < jsonData.Count; i++) { 
            string str = jsonData[i]["line"].ToString();
            int num = (int)jsonData[i]["id"];
            dic.Add(num, str);
        }

        return dic;

    }
}
